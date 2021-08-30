using Forum.Backend.Core.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Backend.Infrastructure.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(e => e.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(e => e.PasswordSalt)
                .HasColumnName("password_salt")
                .IsRequired();

            builder.Property(e => e.TimeZone)
                .HasColumnName("timezone")
                .IsRequired();

            builder.Property(e => e.LastLoginDateUtc)
                .HasColumnName("last_login_date_utc")
                .IsRequired(false);

            builder.ToTable("users");
        }
    }
}
