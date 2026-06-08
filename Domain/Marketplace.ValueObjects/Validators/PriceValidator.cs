using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Exceptions;


namespace Marketplace.ValueObjects.Validators;
public class PriceValidator : IValidator<decimal>
{
    public void Validate(decimal value)
    {
        if (value < 0)
            throw new NegativePriceException(value);
    }
}