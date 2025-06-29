using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Vota.Domain.UserDetails;

namespace Vota.EF
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class VotaDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        /// <summary>
        /// User details.
        /// </summary>
        public DbSet<UserDetail> UserDetails { get; set; }


        /// <summary>
        /// Creates context.
        /// </summary>
        /// <param name="options">Context options.</param>
        public VotaDbContext(DbContextOptions<VotaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure UserDetail relationship
            modelBuilder.Entity<UserDetail>()
            .HasOne(ud => ud.User)
            .WithOne()
            .HasForeignKey<UserDetail>(ud => ud.UserId)
            .IsRequired();


            //Note: this configurations is done for PostgreSQL database, for sql server no need to configure 
            // for identity tables.


            // Configure IdentityUser
            modelBuilder.Entity<IdentityUser<int>>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd(); // Auto-increment for User Id

            // Configure IdentityRole
            modelBuilder.Entity<IdentityRole<int>>()
                .HasKey(ir => ir.Id); // Primary key for Role

            // Configure IdentityUserRole
            modelBuilder.Entity<IdentityUserRole<int>>()
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // Composite key for UserRole

            // Configure IdentityUserClaim
            modelBuilder.Entity<IdentityUserClaim<int>>()
                .Property(uc => uc.Id)
                .ValueGeneratedOnAdd(); // Auto-increment for UserClaim Id

            // Configure IdentityUserLogin
            modelBuilder.Entity<IdentityUserLogin<int>>()
                .HasKey(ul => new { ul.LoginProvider, ul.ProviderKey }); // Composite key for UserLogin

            // Configure IdentityRoleClaim
            modelBuilder.Entity<IdentityRoleClaim<int>>()
                .Property(rc => rc.Id)
                .ValueGeneratedOnAdd(); // Auto-increment for RoleClaim Id

            // Configure IdentityUserToken
            modelBuilder.Entity<IdentityUserToken<int>>()
                .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name }); // Composite key for UserToken

            // Apply additional configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetAssembly(typeof(VotaDbContext)));
        }
    }
}
