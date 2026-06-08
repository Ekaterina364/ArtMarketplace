using Marketplace.Domain.Base;

namespace Marketplace.Domain.Entities;

public class Download(Guid id, Purchase purchase) : Entity<Guid>(id)
{
    public Purchase Purchase { get; private set; } = purchase;
    public DateTime DownloadedAt { get; private set; } = DateTime.UtcNow;
}