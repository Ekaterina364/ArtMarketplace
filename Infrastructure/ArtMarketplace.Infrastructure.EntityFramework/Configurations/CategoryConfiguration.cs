using Marketplace.Domain.Entities;
using Marketplace.ValueObjects;
using Marketplace.ValueObjects.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtMarketplace.Infrastructure.EntityFramework.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(name => name.Value, str => new CategoryName(str))
            .HasMaxLength(CategoryNameValidator.MAX_LENGTH);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasConversion(
                desc => desc == null ? null : desc.Value,
                str => str == null ? null : new CategoryDescription(str))
            .HasMaxLength(CategoryDescriptionValidator.MAX_LENGTH);
    }
}