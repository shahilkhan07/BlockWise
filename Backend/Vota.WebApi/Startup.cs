using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;
using Vota.Domain.UserDetails;
using Vota.EF;
using Vota.EF.Repositories;
using Vota.EF.Services;
using Vota.WebApi.AIServices;
using Vota.WebApi.Common;
using Vota.WebApi.Extensions;
using Vota.WebApi.Middleware;
using Vota.WebApi.Utilities;

namespace Vota.WebApi
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Creates startup instance.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures application services.
        /// </summary>
        /// <param name="services">Services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            if (IsEnablesApplicationInsights())
            {
                services.AddApplicationInsightsTelemetry();
            }

            services.AddMvc();
            services.AddCors();

            ConfigurePostgresContext(services);

            services.AddAutoMapper(typeof(VotatAutoMapperProfile));

            services.AddHttpClient();

            services
                .AddControllers()
                .AddApplicationPart(Assembly.GetAssembly(typeof(ITController)));

            AddVersioning(services);

            services.AddSwagger(Configuration);

            AddServices(services);
            AddAuthorization(services);
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Environment.</param>
        /// <param name="apiVersionDescriptionProvider">API version description provider.</param>
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseCors(options =>
            {
                options
                    .WithOrigins(Configuration.GetValue<string>("AllowedHosts"))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            //if (IsEnablesApplicationInsights())
            //{
            //    app.UseRequestBodyLogging();
            //    app.UseResponseBodyLogging();
            //}

            UseDatabaseMigrations(app);

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseSwagger(apiVersionDescriptionProvider, Configuration);
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
#if DEBUG
                endpoints.MapControllers();
#else
                endpoints.MapControllers();
#endif
            });
        }

        private void ConfigurePostgresContext(IServiceCollection services)
        {
            // Get the base connection string from the configuration
            var connection = Configuration.GetConnectionString("VotaPostgres");

            // Check if we need to override the connection string with an environment variable
            if (IsInjectPostgresConnectionString())
            {
                connection = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION");
            }

            // Register the DbContext with PostgreSQL configuration
            services.AddDbContext<VotaDbContext>(options =>
            options.UseNpgsql(
                connection,
                o =>
                {
                    o.UseNetTopologySuite();  // Enable PostGIS (geospatial support)
                    o.EnableRetryOnFailure(3); // Retry on transient failures
                }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // No tracking for read-only queries
            );

            // ===== Add Identity ========
            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>()
                .AddEntityFrameworkStores<VotaDbContext>()
                .AddDefaultTokenProviders();    
        }


        private void AddServices(IServiceCollection services)
        {
            services.AddTransient<IDbMigrationService, RhodeSideAssistDbMigrationService>();
            //services.AddTransient<IEmptyRepeatableJobsService, EmptyRepeatableJobsService>();

            services.AddTransient<IUserDetailRepository, UserDetailRepository>();
            services.AddTransient<IUserDetailService, UserDetailService>();

            services.AddTransient<CryptoUtils>();
            services.AddSingleton<UserContext>();
            services.AddScoped<UserContextProvider>();
            services.AddHttpClient<IGeminiService, GeminiService>();
        }


        /// <summary>
        /// Adds authorization.
        /// </summary>
        /// <param name="services">Services.</param>
        protected virtual void AddAuthorization(IServiceCollection services)
        {
           
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
            services.AddControllers();
        }

        private bool IsEnablesApplicationInsights()
        {
            return Configuration.GetValue<bool>("EnablesApplicationInsights");
        }

        private static void AddVersioning(IServiceCollection services)
        {
            var apiVersioningBuilder = services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                // Use whatever reader you want
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                new HeaderApiVersionReader("x-api-version"),
                                                new MediaTypeApiVersionReader("x-api-version"));
            }); // Nuget Package: Asp.Versioning.Mvc

            apiVersioningBuilder.AddApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            }); // Nuget Package: Asp.Versioning.Mvc.ApiExplorer
        }

        private void UseDatabaseMigrations(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IDbMigrationService>().MigrateAsync().Wait();
            }
        }

        private bool IsInjectPostgresConnectionString()
        {
            return Configuration.GetValue<bool>("IsInjectPostgresConnectionString");
        }
    }
}
