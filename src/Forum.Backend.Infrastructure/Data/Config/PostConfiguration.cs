using Forum.Backend.Core.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Backend.Infrastructure.Data.Config
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Message)
                .HasColumnName("message")
                .IsRequired();

            builder.Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(e => e.CategoryId)
                .HasColumnName("category_id")
                .IsRequired();

            builder.Property(e => e.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();

            builder.Property(e => e.UpdatedAtUtc)
                .HasColumnName("updated_at_utc")
                .IsRequired(false);

            builder.HasOne(e => e.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(e => e.CategoryId);

            builder.HasOne(e => e.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(e => e.UserId);

            builder.ToTable("posts");
        }
    }
}
