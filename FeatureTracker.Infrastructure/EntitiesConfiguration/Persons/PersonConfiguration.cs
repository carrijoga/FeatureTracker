using FeatureTracker.Domain.Model.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.ModelConfiguration.Persons;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable(nameof(Person));
        builder.HasKey(x => x.PersonId);

        builder.Property(x => x.FirstName)
            .HasColumnName(nameof(Person.FirstName))
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnName(nameof(Person.LastName))
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasColumnName(nameof(Person.Phone));

        builder.Property(x => x.BirthDay)
            .HasColumnName(nameof(Person.BirthDay));

        builder.Property(x => x.Cpf)
            .HasColumnName(nameof(Person.Cpf));

        builder.Property(x => x.Cnpj)
            .HasColumnName(nameof(Person.Cnpj));

        builder.HasMany(x => x.Types)
            .WithOne(x => x.Person)
            .HasForeignKey(x => x.PersonId);

        builder.HasMany(x => x.Address)
            .WithOne(x => x.Person)
            .HasForeignKey(x => x.PersonId);
    }
}
