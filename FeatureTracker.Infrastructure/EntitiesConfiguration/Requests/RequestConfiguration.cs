using FeatureTracker.Domain.Model.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.EntitiesConfiguration.Requests;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable(nameof(Request));
        builder.HasKey(x => x.RequestId);

        builder.Property(x => x.Title)
            .HasColumnName(nameof(Request.Title))
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasColumnName(nameof(Request.Description))
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Status)
            .HasColumnName(nameof(Request.Status))
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName(nameof(Request.CreatedAt))
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName(nameof(Request.UpdatedAt))
            .IsRequired(false);

        builder.HasOne(x => x.Category)
            .WithOne()
            .HasForeignKey<Request>(x => x.CategoryId);

        builder.HasOne(x => x.Team)
            .WithOne()
            .HasForeignKey<Request>(x => x.TeamId);
    }
}
