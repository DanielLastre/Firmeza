using Firmeza.Domain.Entities;
using Firmeza.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firmeza.Infrastructure.Persistence.Configurations;

public class SaleDetailConfiguration: IEntityTypeConfiguration<SaleDetail>
{
    public void Configure(EntityTypeBuilder<SaleDetail> builder)
    {
        builder.HasKey(sd => sd.Id);

        builder.Property(sd => sd.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(sd => sd.ProductId)
            .IsRequired();

        builder.Property(sd => sd.Quantity)
            .IsRequired();

        builder.Property(sd => sd.Price)
            .HasConversion(
                money => money.Amount,
                amount => Money.Create(amount))
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(sd => sd.Subtotal)
            .HasConversion(
                money => money.Amount,
                amount => Money.Create(amount))
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(sd => sd.SaleId)
            .IsRequired();

        builder.HasOne(sd => sd.Product)
            .WithMany()
            .HasForeignKey(sd => sd.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sd => sd.Sale)
            .WithMany(s => s.SaleDetails)
            .HasForeignKey(sd => sd.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}