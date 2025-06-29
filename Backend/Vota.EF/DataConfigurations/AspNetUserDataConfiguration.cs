//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Vota.EF.DataConfigurations
//{
//    public class AspNetUserDataConfiguration : IEntityTypeConfiguration<IdentityUser<int>>
//    {
//        public void Configure(EntityTypeBuilder<IdentityUser<int>> builder)
//        {
//            // Password hashing
//            var passwordHasher = new PasswordHasher<IdentityUser<int>>();

//            // Create the user first
//            var user = new IdentityUser<int>
//            {
//                Id = 1,
//                UserName = "natrixsoftware",
//                //NormalizedUserName = "NATRIXSOFTWARE",
//                Email = "natrixsoftware@example.com",
//                NormalizedEmail = "NATRIXSOFTWARE@EXAMPLE.COM",
//                EmailConfirmed = true,
//                PasswordHash = passwordHasher.HashPassword(null, "N@trix2025!"),
//                SecurityStamp = "3d7b7ddb-775d-46ac-9b58-ab13d818a1cf",
//                ConcurrencyStamp = "9362ea0a-2672-4df7-927f-0a71bfa81f6f",
//                PhoneNumber = "1234567890",
//                PhoneNumberConfirmed = true,
//                TwoFactorEnabled = true,
//            };

//            builder.HasData(user);
//        }
//    }

//    public class AspNetUserRoleDataConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
//    {
//        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
//        {
//            builder.HasData(
//                new IdentityUserRole<int>
//                {
//                    UserId = 1,
//                    RoleId = 1 // SuperAdmin role
//                }
//            );
//        }
//    }
//}