using Firmeza.Domain.Entities;
using Firmeza.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firmeza.Infrastructure.Persistence.Configurations;

public class EnterpriseConfiguration: IEntityTypeConfiguration<Enterprise>
{
    public void Configure(EntityTypeBuilder<Enterprise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(e => e.TradeName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(e => e.Type)
            .IsRequired();

        builder.Property(e => e.Address)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.Phone)
            .HasConversion(
                phone => phone.Value,
                value => PhoneNumber.Create(value))
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.TaxId)
            .HasConversion(
                taxId => taxId.Value,
                value => TaxId.Create(value))
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(e => e.TaxId)
            .IsUnique()
            .HasDatabaseName("IX_Enterprises_TaxId");

        builder.HasIndex(e => e.Email)
            .HasDatabaseName("IX_Enterprises_Email");
    }
}