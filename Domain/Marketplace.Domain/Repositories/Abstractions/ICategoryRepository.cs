using Marketplace.Domain.Entities;
using Marketplace.Domain.Interfaces.Repositories.Base;

namespace Marketplace.Domain.Interfaces.Repositories;

public interface ICategoryRepository : IRepository<Category, Guid>
{
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken);
}