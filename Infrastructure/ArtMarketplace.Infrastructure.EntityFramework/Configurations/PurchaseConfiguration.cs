using Marketplace.Domain.Entities;
using Marketplace.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtMarketplace.Infrastructure.EntityFramework.Configurations;
public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.PurchasePrice)
            .IsRequired()
            .HasConversion(price => price.Value, value => new Price(value));

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.PurchasedAt)
            .IsRequired()
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );

        builder.HasOne(x => x.Buyer)
            .WithMany("_purchases")
            .HasForeignKey("BuyerId")
            .HasPrincipalKey(x => x.Id);

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("ProductId")
            .HasPrincipalKey(x => x.Id);

        builder.HasMany<Download>("_downloads")
            .WithOne(x => x.Purchase)
            .HasForeignKey("PurchaseId")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.Downloads);
    }
}
