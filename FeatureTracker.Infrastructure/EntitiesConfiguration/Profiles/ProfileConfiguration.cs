using FeatureTracker.Domain.Model.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.EntitiesConfiguration.Profiles;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable(nameof(Profile));
        builder.HasKey(x => x.ProfileId);

        builder.Property(x => x.Description)
            .HasColumnName(nameof(Profile.Description))
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .HasColumnName(nameof(Profile.CreatedAt))
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.CreatedBy)
            .HasColumnName(nameof(Profile.CreatedBy))
            .IsRequired();

        builder.HasMany(x => x.Screens)
            .WithOne(x => x.Profile)
            .HasForeignKey(x => x.ProfileScreensId);

    }
}
