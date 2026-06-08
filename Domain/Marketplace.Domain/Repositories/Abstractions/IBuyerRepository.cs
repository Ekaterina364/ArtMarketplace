using Marketplace.Domain.Entities;
using Marketplace.Domain.Interfaces.Repositories.Base;
using Marketplace.ValueObjects;

namespace Marketplace.Domain.Interfaces.Repositories;

public interface IBuyerRepository : IRepository<Buyer, Guid>
{
    Task<Buyer?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
}
