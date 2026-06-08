using Marketplace.Domain.Entities;
using Marketplace.Domain.Interfaces.Repositories.Base;
using Marketplace.ValueObjects;

namespace Marketplace.Domain.Interfaces.Repositories;

public interface IArtistRepository : IRepository<Artist, Guid>
{
    Task<Artist?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
}