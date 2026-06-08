using Marketplace.Domain.Entities;

namespace Marketplace.Domain.Exceptions;

public class ProductNotReadyForPublishException(Product product, string reason)
    : InvalidOperationException($"Product {product.Id} cannot be published: {reason}")
{
    public Product ProductId => product;
    public string Reason => reason;
}