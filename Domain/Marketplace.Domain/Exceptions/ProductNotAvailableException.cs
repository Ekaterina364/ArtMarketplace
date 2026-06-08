using Marketplace.Domain.Entities;

namespace Marketplace.Domain.Exceptions;

public class ProductNotAvailableException(Product product)
    : InvalidOperationException($"Product {product.Id} is not available for purchase")
{
    public Product ProductId => product;
}