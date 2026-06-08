using Marketplace.Domain.Entities;
using Marketplace.Domain.Interfaces.Repositories.Base;

namespace Marketplace.Domain.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product, Guid>
{
    Task<IReadOnlyList<Product>> GetByArtistIdAsync(Guid artistId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Product>> GetPublishedProductsAsync(CancellationToken cancellationToken);
}