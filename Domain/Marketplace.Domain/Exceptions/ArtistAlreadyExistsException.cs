namespace Marketplace.Domain.Exceptions;

public class ArtistAlreadyExistsException(string email)
    : InvalidOperationException($"Artist with email {email} already exists")
{
    public string Email => email;
}