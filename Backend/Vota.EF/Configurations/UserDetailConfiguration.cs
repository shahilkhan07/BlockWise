using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vota.Domain.UserDetails;

namespace Vota.EF.Configurations
{
    public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.ToTable("UserDetails");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                   .HasMaxLength(50).
                   HasColumnName("FirstName")
                   .IsRequired(false);

            builder.Property(x => x.LastName)
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(x => x.ProfileBio)
                   .HasMaxLength(1000);

            builder.Property(x => x.ProfileImageUrl)
                   .HasMaxLength(255);

            builder.Property(x => x.CreatedAt)
                   .HasColumnType("timestamp with time zone")
                   .IsRequired();

            builder.Property(x => x.ModifiedAt)
                   .HasColumnType("timestamp with time zone");
                   //.IsRequired();

            builder.Property(x => x.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);
        }
    }
}