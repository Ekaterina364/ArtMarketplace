using Marketplace.Domain.Base;
using Marketplace.Domain.Enums;
using Marketplace.Domain.Exceptions;
using Marketplace.ValueObjects;

namespace Marketplace.Domain.Entities;

public class Purchase : Entity<Guid>
{
    public Buyer Buyer { get; private set; } = null!;
    public Product Product { get; private set; } = null!;
    public Price PurchasePrice { get; private set; }
    public PurchaseStatus Status { get; private set; }
    public DateTime PurchasedAt { get; private set; }

    private readonly ICollection<Download> _downloads = [];
    public IReadOnlyCollection<Download> Downloads => (IReadOnlyCollection<Download>)_downloads;

    protected Purchase() { }

    public Purchase(Guid id, Buyer buyer, Product product, Price purchasePrice) : base(id)
    {
        Buyer = buyer ?? throw new ArgumentNullValueException(nameof(buyer));
        Product = product ?? throw new ArgumentNullValueException(nameof(product));
        PurchasePrice = purchasePrice ?? throw new ArgumentNullValueException(nameof(purchasePrice));
        Status = PurchaseStatus.Pending;
        PurchasedAt = DateTime.UtcNow;
    }

    public bool MarkAsPaid()
    {
        if (Status == PurchaseStatus.Paid) return false;
        if (Status == PurchaseStatus.Canceled)
            throw new InvalidOperationException("Cannot pay for a canceled purchase");
        Status = PurchaseStatus.Paid;
        return true;
    }

    public bool Cancel()
    {
        if (Status == PurchaseStatus.Canceled) return false;
        if (Status == PurchaseStatus.Paid)
            throw new InvalidOperationException("Cannot cancel a paid purchase");
        Status = PurchaseStatus.Canceled;
        return true;
    }

    public Download CreateDownload()
    {
        if (Status != PurchaseStatus.Paid)
            throw new DownloadNotAllowedException(this);
        var download = new Download(Guid.NewGuid(), this);
        _downloads.Add(download);
        return download;
    }
}