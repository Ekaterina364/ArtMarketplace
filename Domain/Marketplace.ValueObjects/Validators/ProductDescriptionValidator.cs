using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Exceptions;

namespace Marketplace.ValueObjects.Validators;
public class ProductDescriptionValidator : IValidator<string?>
{
    public const int MAX_LENGTH = 1000;
    public void Validate(string? value)
    {
        if (value == null) return;

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}