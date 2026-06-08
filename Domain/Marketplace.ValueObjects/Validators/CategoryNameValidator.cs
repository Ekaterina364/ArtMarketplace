using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Exceptions;

namespace Marketplace.ValueObjects.Validators;

public class CategoryNameValidator : IValidator<string>
{
    public const int MAX_LENGTH = 50;
    public const int MIN_LENGTH = 2;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);

        if (value.Length < MIN_LENGTH)
            throw new ArgumentShortValueException(nameof(value), value, MIN_LENGTH);
    }
}