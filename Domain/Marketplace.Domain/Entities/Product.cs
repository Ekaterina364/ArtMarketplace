using Marketplace.Domain.Base;
using Marketplace.Domain.Enums;
using Marketplace.Domain.Exceptions;
using Marketplace.ValueObjects;

namespace Marketplace.Domain.Entities;

public class Product : Entity<Guid>
{

    public Artist Artist { get; private set; }
    public Category Category { get; private set; }
    public ProductTitle Title { get; private set; }
    public ProductDescription? Description { get; private set; }
    public ProductStatus Status { get; private set; }
    public int ViewsCount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastModifiedAt { get; private set; }


    public Price? Price { get; private set; }
    public FileUrl? FileUrl { get; private set; }

    protected Product() { }

    public Product(
       Artist artist,
       Category category,
       ProductTitle title,
       ProductDescription? description = null)
       : this(Guid.NewGuid(), artist, category, title, description) { }

    protected Product(
        Guid id,
        Artist artist,
        Category category,
        ProductTitle title,
        ProductDescription? description,
        ProductStatus? status = null,
        int? viewsCount = null,
        DateTime? createdAt = null,
        DateTime? lastModifiedAt = null,
        Price? price = null,
        FileUrl? fileUrl = null)
        : base(id)
    {
        Artist = artist ?? throw new ArgumentNullValueException(nameof(artist));
        Category = category ?? throw new ArgumentNullValueException(nameof(category));
        Title = title ?? throw new ArgumentNullValueException(nameof(title));
        Description = description;
        Status = status ?? ProductStatus.Draft;
        ViewsCount = viewsCount ?? 0;
        CreatedAt = createdAt ?? DateTime.UtcNow;
        LastModifiedAt = lastModifiedAt ?? DateTime.UtcNow;
        Price = price;
        FileUrl = fileUrl;
    }

    public bool SetTitle(ProductTitle newTitle)
    {
        if (Title == newTitle) return false;
        Title = newTitle ?? throw new ArgumentNullValueException(nameof(newTitle));
        return true;
    }

    public bool SetDescription(ProductDescription? newDescription)
    {
        if (Description == newDescription) return false;
        Description = newDescription;
        return true;
    }

    public bool SetPrice(Price newPrice)
    {
        if (Price == newPrice) return false;
        Price = newPrice ?? throw new ArgumentNullValueException(nameof(newPrice));
        return true;
    }

    public bool UploadFile(FileUrl newFileUrl)
    {
        if (FileUrl == newFileUrl) return false;
        FileUrl = newFileUrl ?? throw new ArgumentNullValueException(nameof(newFileUrl));
        return true;
    }

    public void Publish()
    {
        if (Price == null) throw new ProductNotReadyForPublishException(this, "Price not set");
        if (FileUrl == null) throw new ProductNotReadyForPublishException(this, "File not uploaded");
        Status = ProductStatus.Completed;
    }
    public bool IsPublished()
    {
        return Status == ProductStatus.Completed;
    }

    public void SetModified(DateTime utcNow) => LastModifiedAt = utcNow;

    public void IncrementViews()
    {
        if (Status == ProductStatus.Completed)
            ViewsCount++;
    }
}