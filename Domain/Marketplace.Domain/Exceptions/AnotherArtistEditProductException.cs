using Marketplace.Domain.Entities;

namespace Marketplace.Domain.Exceptions;

public class AnotherArtistEditProductException(Product product, Artist artist)
    : InvalidOperationException($"The artist {artist.Username} can't edit the product {product.Title} owned by the artist {product.Artist} (product id = {product.Id}).")
{
    public Product Product => product;
    public Artist Artist => artist;
}