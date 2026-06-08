using Marketplace.Domain.Base;
using Marketplace.Domain.Enums;
using Marketplace.Domain.Exceptions;
using Marketplace.ValueObjects;

namespace Marketplace.Domain.Entities;

public class Purchase(Guid id, Buyer buyer, Product product, Price purchasePrice) : Entity<Guid>(id)
{
    public Buyer Buyer { get; private set; } = buyer;
    public Product Product { get; private set; } = product;
    public Price PurchasePrice { get; private set; } = purchasePrice ?? throw new ArgumentNullValueException(nameof(purchasePrice));
    public PurchaseStatus Status { get; private set; } = PurchaseStatus.Pending;
    public DateTime PurchasedAt { get; private set; } = DateTime.UtcNow;

    private readonly ICollection<Download> _downloads = [];
    public IReadOnlyCollection<Download> Downloads => (IReadOnlyCollection<Download>)_downloads;

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