using FeatureTracker.Domain.Model.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.EntitiesConfiguration.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category));
        builder.HasKey(x => x.CategoryId);

        builder.Property(x => x.CategoryName)
            .HasColumnName(nameof(Category.CategoryName))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CategoryEmoji)
            .HasColumnName(nameof(Category.CategoryEmoji))
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .HasColumnName(nameof(Category.CreatedAt))
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName(nameof(Category.UpdatedAt))
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasColumnName(nameof(Category.UpdatedBy))
            .IsRequired(false);
    }
}
