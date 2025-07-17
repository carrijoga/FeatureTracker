using FeatureTracker.Domain.Model.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.ModelConfiguration.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(Users));
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.Email)
            .HasColumnName(nameof(User.Email))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Username)
            .HasColumnName(nameof(User.Username))
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.CreatedAt)
            .HasColumnName(nameof(User.CreatedAt))
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(x => x.Person)
            .WithMany()
            .HasForeignKey(x => x.PersonId);

        builder.HasOne(x => x.Profile)
            .WithMany()
            .HasForeignKey(x => x.ProfileId);

        //builder.HasOne(x => x.Company)
        //    .WithOne()
        //    .HasForeignKey<User>(x => x.CompanyId);

        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.Username).IsUnique();
    }
}
