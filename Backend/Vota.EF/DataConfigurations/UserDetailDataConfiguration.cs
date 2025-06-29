//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Vota.Domain.UserDetails;

//namespace Vota.EF.DataConfigurations
//{
//    public class UserDetailDataConfiguration : IEntityTypeConfiguration<UserDetail>
//    {
//        public void Configure(EntityTypeBuilder<UserDetail> builder)
//        {
//            builder.HasData(
//                Create(1, 1, "Natrix", "Software")
//            );
//        }

//        private UserDetail Create(int id, int userId, string firstName, string lastName)
//        {
//            return new UserDetail
//            {
//                Id = 1,
//                UserId = userId,
//                FirstName = firstName,
//                LastName = lastName,
//                IsActive = true,
//            };
//        }
//    }
//}
