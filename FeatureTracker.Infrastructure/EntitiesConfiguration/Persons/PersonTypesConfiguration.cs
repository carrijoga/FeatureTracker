using FeatureTracker.Domain.Model.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.ModelConfiguration.Persons;

public class PersonTypesConfiguration : IEntityTypeConfiguration<PersonType>
{
    public void Configure(EntityTypeBuilder<PersonType> builder)
    {
        builder.ToTable(nameof(PersonType));
        builder.HasKey(x => x.PersonTypeId);

        builder.Property(x => x.Type)
            .HasColumnName(nameof(PersonType.Type))
            .IsRequired();

        builder.HasOne(x => x.Person)
            .WithMany(x => x.Types)
            .HasForeignKey(x => x.PersonId);
    }
}
