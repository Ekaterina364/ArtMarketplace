using Marketplace.Domain.Base;
using Marketplace.Domain.Exceptions;

namespace Marketplace.Domain.Entities;

public class Download : Entity<Guid>
{
    public Purchase Purchase { get; private set; } = null!;
    public DateTime DownloadedAt { get; private set; }

    protected Download() { }

    public Download(Guid id, Purchase purchase) : base(id)
    {
        Purchase = purchase ?? throw new ArgumentNullValueException(nameof(purchase));
        DownloadedAt = DateTime.UtcNow;
    }
}