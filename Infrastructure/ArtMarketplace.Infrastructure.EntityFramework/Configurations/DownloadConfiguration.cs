using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ArtMarketplace.Infrastructure.EntityFramework.Configurations;
public class DownloadConfiguration : IEntityTypeConfiguration<Download>
{
    public void Configure(EntityTypeBuilder<Download> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.DownloadedAt)
            .IsRequired()
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );

        builder.HasOne(x => x.Purchase)
            .WithMany("_downloads")
            .HasForeignKey("PurchaseId")
            .HasPrincipalKey(x => x.Id);
    }
}