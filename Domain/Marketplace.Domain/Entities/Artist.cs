using Marketplace.Domain.Base;
using Marketplace.Domain.Exceptions;
using Marketplace.ValueObjects;

namespace Marketplace.Domain.Entities;

public class Artist(Guid id, Username username, Email email) : Entity<Guid>(id)
{
    private readonly ICollection<Product> _products = [];

    public Username Username { get; private set; } = username ?? throw new ArgumentNullValueException(nameof(username));
    public Email Email { get; private set; } = email ?? throw new ArgumentNullValueException(nameof(email));
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public IReadOnlyCollection<Product> Products => (IReadOnlyCollection<Product>)_products;

    internal bool ChangeUsername(Username newUsername)
    {
        if (newUsername == null) throw new ArgumentNullValueException(nameof(newUsername));
        if (Username == newUsername) return false;
        Username = newUsername;
        return true;
    }

 
    public Product CreateProduct(Category category, ProductTitle title, ProductDescription? description)
    {
        var product = new Product(this, category, title, description);
        _products.Add(product);
        return product;
    }

    public bool EditProduct(Product product, ProductTitle newTitle, ProductDescription? newDescription, Price? newPrice, FileUrl? newFileUrl)
    {
        if (product.Artist != this)
            throw new AnotherArtistEditProductException(product, this);
        if (!_products.Contains(product))
            throw new ProductNotBelongArtistException(product, this);

        bool isEdit = false;

        if (newTitle != null && product.Title != newTitle)
        {
            product.SetTitle(newTitle);
            isEdit = true;
        }

        if (newDescription != null && product.Description != newDescription)
        {
            product.SetDescription(newDescription);
            isEdit = true;
        }

        if (newPrice != null && product.Price != newPrice)
        {
            product.SetPrice(newPrice);
            isEdit = true;
        }

        if (newFileUrl != null && product.FileUrl != newFileUrl)
        {
            product.UploadFile(newFileUrl);
            isEdit = true;
        }
        
        if (isEdit) product.SetModified(DateTime.UtcNow);

        return isEdit;
    }

    public void DeleteProduct(Product product)
    {
        if (product.Artist != this) throw new AnotherArtistDeleteProductException(product, this); 
        if (!_products.Contains(product)) throw new ProductNotBelongArtistException(product, this);
        _products.Remove(product);
    }
    public void PublishProduct(Product product)
    {
        if (product.Artist != this) throw new AnotherArtistPublishProductException(product, this);
        product.Publish();
        product.SetModified(DateTime.UtcNow);
    }
}