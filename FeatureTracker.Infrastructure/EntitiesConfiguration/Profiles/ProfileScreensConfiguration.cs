using FeatureTracker.Domain.Model.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.EntitiesConfiguration.Profiles;

public class ProfileScreensConfiguration : IEntityTypeConfiguration<ProfileScreens>
{
    public void Configure(EntityTypeBuilder<ProfileScreens> builder)
    {
        builder.ToTable(nameof(ProfileScreens));
        builder.HasKey(x => x.ProfileScreensId);

        builder.HasOne(x => x.Profile)
            .WithMany(x => x.Screens)
            .HasForeignKey(x => x.ProfileScreensId);
    }
}
