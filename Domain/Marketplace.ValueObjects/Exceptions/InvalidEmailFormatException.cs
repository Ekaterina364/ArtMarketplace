namespace Marketplace.ValueObjects.Exceptions;

public class InvalidEmailFormatException(string email)
    : FormatException($"Invalid email format: '{email}'.")
{
    public string Email => email;
}