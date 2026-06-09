using Marketplace.Domain.Entities;
using Marketplace.ValueObjects;
using Marketplace.ValueObjects.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArtMarketplace.Infrastructure.EntityFramework.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasConversion(title => title.Value, str => new ProductTitle(str))
            .HasMaxLength(ProductTitleValidator.MAX_LENGTH);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasConversion(
                desc => desc == null ? null : desc.Value,
                str => str == null ? null : new ProductDescription(str))
            .HasMaxLength(ProductDescriptionValidator.MAX_LENGTH);

        builder.Property(x => x.Price)
            .IsRequired(false)
            .HasConversion(new ValueConverter<Price?, decimal?>(
                v => v == null ? null : v.Value,
                v => v == null ? null : new Price(v.Value)
            ));

        builder.Property(x => x.FileUrl)
            .IsRequired(false)
            .HasConversion(
                url => url == null ? null : url.Value,
                str => str == null ? null : new FileUrl(str))
            .HasMaxLength(FileUrlValidator.MAX_LENGTH);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.ViewsCount).IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );

        builder.Property(x => x.LastModifiedAt)
            .IsRequired()
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );

        builder.HasOne(x => x.Artist)
            .WithMany("_products")
            .HasForeignKey("ArtistId")
            .HasPrincipalKey(x => x.Id);

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey("CategoryId")
            .HasPrincipalKey(x => x.Id);
    }
}