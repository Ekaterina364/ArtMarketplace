using Marketplace.Domain.Entities;
using Marketplace.ValueObjects;
using Marketplace.ValueObjects.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtMarketplace.Infrastructure.EntityFramework.Configurations;
public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Username)
            .IsRequired()
            .HasConversion(username => username.Value, str => new Username(str))
            .HasMaxLength(UsernameValidator.MAX_LENGTH);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasConversion(email => email.Value, str => new Email(str))
            .HasMaxLength(EmailValidator.MAX_LENGTH);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );

        builder.HasMany<Product>("_products")
            .WithOne(x => x.Artist)
            .HasForeignKey("ArtistId")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.Products);
    }
}

