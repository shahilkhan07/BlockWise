using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vota.EF.DataConfigurations
{
    public class AspNetRoleDataConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
            // Seeding roles
            builder.HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "dca709e6-e6b3-496c-95e6-9bca7e4fb2b5" },
                new IdentityRole<int> { Id = 2, Name = "Influencer", NormalizedName = "INFLUENCER", ConcurrencyStamp = "4a9cc101-01a6-4a79-a244-1c06cb452991" },
                new IdentityRole<int> { Id = 3, Name = "User", NormalizedName = "USER" , ConcurrencyStamp = "ba51f770-80c6-4bb9-8549-bc8b01ce2c8d" }
            );
        }
    }
}