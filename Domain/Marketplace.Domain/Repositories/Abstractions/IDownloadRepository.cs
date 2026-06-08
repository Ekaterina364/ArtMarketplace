using Marketplace.Domain.Entities;
using Marketplace.Domain.Interfaces.Repositories.Base;

namespace Marketplace.Domain.Interfaces.Repositories;

public interface IDownloadRepository : IRepository<Download, Guid>
{
    Task<IReadOnlyList<Download>> GetByPurchaseIdAsync(Guid purchaseId, CancellationToken cancellationToken);
}