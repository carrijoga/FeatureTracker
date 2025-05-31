using FeatureTracker.Domain.Model.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.ModelConfiguration.Persons;

public class PersonAddressConfiguration : IEntityTypeConfiguration<PersonAddress>
{
    public void Configure(EntityTypeBuilder<PersonAddress> builder)
    {
        builder.ToTable(nameof(PersonAddress));
        builder.HasKey(x => x.PersonAddressId);

        builder.Property(x => x.Street)
            .HasColumnName(nameof(PersonAddress.Street))
            .IsRequired();

        builder.Property(x => x.Number)
            .HasColumnName(nameof(PersonAddress.Number))
            .IsRequired();

        builder.Property(x => x.Complement)
            .HasColumnName(nameof(PersonAddress.Complement));

        builder.Property(x => x.Neighborhood)
            .HasColumnName(nameof(PersonAddress.Neighborhood))
            .IsRequired();

        builder.Property(x => x.City)
            .HasColumnName(nameof(PersonAddress.City))
            .IsRequired();

        builder.Property(x => x.State)
            .HasColumnName(nameof(PersonAddress.State))
            .IsRequired();

        builder.Property(x => x.Country)
            .HasColumnName(nameof(PersonAddress.Country))
            .IsRequired();

        builder.Property(x => x.ZipCode)
            .HasColumnName(nameof(PersonAddress.ZipCode))
            .IsRequired();

        builder.Property(x => x.Reference)
            .HasColumnName(nameof(PersonAddress.Reference));

        builder.Property(x => x.IsMain)
            .HasColumnName(nameof(PersonAddress.IsMain))
            .IsRequired();

        builder.HasOne(x => x.Person)
            .WithMany(x => x.Address)
            .HasForeignKey(x => x.PersonId);
    }
}
