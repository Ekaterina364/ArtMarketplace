namespace Marketplace.ValueObjects.Exceptions;

public class NegativePriceException(decimal value)
    : FormatException($"Price cannot be negative: {value}.")
{
    public decimal Value => value;
}