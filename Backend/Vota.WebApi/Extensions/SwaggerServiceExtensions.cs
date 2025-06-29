using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Vota.WebApi.Extensions
{
    /// <summary>
    /// Register the Swagger generator, defining one or more Swagger documents.
    /// </summary>
    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// Add swagger.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            if (IsSwaggerDisabled(configuration))
                return services;

            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }

                // Add XML comments if file exists
                var xmlPath = GetXmlCommentsFilePath();
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

                // Configure OAuth2/Bearer token authentication
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token in the format: Bearer {your token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }

        /// <summary>
        /// Use swagger.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="apiVersionDescriptionProvider">API version description provider.</param>
        /// <param name="configuration">Configuration.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider, IConfiguration configuration)
        {
            if (IsSwaggerDisabled(configuration))
                return app;

            var swaggerBasePath = configuration.GetValue<string>("SwaggerBasePath") ?? string.Empty;

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";

                // Fix server URL configuration
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    var scheme = httpReq.Headers["X-Forwarded-Proto"].FirstOrDefault() ?? httpReq.Scheme;
                    var host = httpReq.Host.Value;
                    var baseUrl = string.IsNullOrEmpty(swaggerBasePath)
                        ? $"{scheme}://{host}"
                        : $"{scheme}://{host}/{swaggerBasePath.TrimStart('/')}";

                    swaggerDoc.Servers = new System.Collections.Generic.List<OpenApiServer>
                    {
                        new OpenApiServer { Url = baseUrl }
                    };
                });
            });

            app.UseSwaggerUI(options =>
            {
                // Add default v1 endpoint
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vota API V1");

                // Add versioned endpoints
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.OrderByDescending(x => x.ApiVersion))
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        $"Vota API {description.GroupName.ToUpperInvariant()}");
                }

                options.RoutePrefix = "swagger";
                options.DocumentTitle = "Vota API Documentation";
            });

            return app;
        }

        private static string GetXmlCommentsFilePath()
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var fileName = typeof(SwaggerServiceExtensions).GetTypeInfo().Assembly.GetName().Name + ".xml";
            return Path.Combine(basePath, fileName);
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = $"Vota API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "Vota service API documentation",
                Contact = new OpenApiContact
                {
                    Name = "Vota Team",
                    Email = "support@vota.com" // Add actual contact email
                },
            };

            if (description.IsDeprecated)
            {
                info.Description += " ⚠️ This API version has been deprecated.";
            }

            return info;
        }

        private static bool IsSwaggerDisabled(IConfiguration configuration)
        {
            return configuration.GetValue<bool>("DisableSwagger");
        }
    }
}