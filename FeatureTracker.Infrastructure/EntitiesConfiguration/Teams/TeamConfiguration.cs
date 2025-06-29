using FeatureTracker.Domain.Model.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.EntitiesConfiguration.Teams;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable(nameof(Team));
        builder.HasKey(t => t.TeamId);

        builder.Property(t => t.TeamName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.UpdatedAt)
            .HasColumnName(nameof(Team.UpdatedAt))
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasColumnName(nameof(Team.UpdatedBy))
            .IsRequired(false);
    }
}
