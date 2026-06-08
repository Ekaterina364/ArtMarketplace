using Marketplace.Domain.Base;
using Marketplace.Domain.Exceptions;
using Marketplace.ValueObjects;

namespace Marketplace.Domain.Entities;

public class Category(Guid id, CategoryName name, CategoryDescription? description) : Entity<Guid>(id)
{
    public CategoryName Name { get; private set; } = name ?? throw new ArgumentNullValueException(nameof(name));
    public CategoryDescription? Description { get; private set; } = description;

    protected Category() : this(Guid.Empty, null!, null) { }

    public bool UpdateName(CategoryName newName)
    {
        if (Name == newName) return false;
        Name = newName ?? throw new ArgumentNullValueException(nameof(newName));
        return true;
    }
    public bool UpdateDescription(CategoryDescription? newDescription)
    {
        if (Description == newDescription) return false;
        Description = newDescription;
        return true;
    }
}