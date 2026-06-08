using Marketplace.Domain.Entities;

namespace Marketplace.Domain.Exceptions;

public class ProductNotBelongArtistException(Product product, Artist artist)
    : InvalidOperationException($"Product {product.Id} does not belong to artist {artist.Id}")
{
    public Product Product => product;
    public Artist Artist => artist;
}