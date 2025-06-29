using FeatureTracker.Domain.Model.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.EntitiesConfiguration.Users;

public class UserInviteConfiguration : IEntityTypeConfiguration<UserInvite>
{
    public void Configure(EntityTypeBuilder<UserInvite> builder)
    {
        builder.ToTable(nameof(UserInvite));
        builder.HasKey(x => x.UserInviteId);
    }
}
