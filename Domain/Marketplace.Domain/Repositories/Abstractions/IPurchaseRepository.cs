using Marketplace.Domain.Entities;
using Marketplace.Domain.Interfaces.Repositories.Base;

namespace Marketplace.Domain.Interfaces.Repositories;

public interface IPurchaseRepository : IRepository<Purchase, Guid>
{
    Task<IReadOnlyList<Purchase>> GetByBuyerIdAsync(Guid buyerId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Purchase>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Purchase>> GetPaidPurchasesByBuyerIdAsync(Guid buyerId, CancellationToken cancellationToken);
}