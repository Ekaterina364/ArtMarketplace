using Marketplace.Domain.Base;
using Marketplace.Domain.Exceptions;
using Marketplace.ValueObjects;
namespace Marketplace.Domain.Entities;

public class Buyer(Guid id, Username username, Email email) : Entity<Guid>(id)
{
    private readonly ICollection<Purchase> _purchases = new List<Purchase>();

    public Username Username { get; private set; } = username ?? throw new ArgumentNullValueException(nameof(username));
    public Email Email { get; private set; } = email ?? throw new ArgumentNullValueException(nameof(email));
    public DateTime RegisteredAt { get; private set; } = DateTime.UtcNow;

    public IReadOnlyCollection<Purchase> Purchases => (IReadOnlyCollection<Purchase>)_purchases;

    internal bool ChangeUsername(Username newUsername)
    {
        if (newUsername == null) throw new ArgumentNullValueException(nameof(newUsername));
        if (Username == newUsername) return false;
        Username = newUsername;
        return true;
    }

    public Purchase PurchaseProduct(Product product, Price priceAtPurchase)
    {
        if (!product.IsPublished()) throw new ProductNotAvailableException(product);
        var purchase = new Purchase(Guid.NewGuid(), this, product, priceAtPurchase);
        _purchases.Add(purchase);
        return purchase;
    }
}