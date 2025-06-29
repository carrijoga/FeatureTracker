using FeatureTracker.Domain.Model.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureTracker.Infrastructure.EntitiesConfiguration.Companies;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable(nameof(Company));
        builder.HasKey(x => x.CompanyId);

        builder.Property(x => x.CompanyName)
            .HasColumnName(nameof(Company.CompanyName))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CPFCNPJ)
            .HasColumnName(nameof(Company.CPFCNPJ))
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.UpdatedAt)
            .HasColumnName(nameof(Company.UpdatedAt))
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasColumnName(nameof(Company.UpdatedBy))
            .IsRequired(false);

        builder.HasIndex(x => x.CPFCNPJ).IsUnique();
    }
}
