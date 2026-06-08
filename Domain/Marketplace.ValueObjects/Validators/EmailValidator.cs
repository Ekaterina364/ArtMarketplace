using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Exceptions;
using System.Text.RegularExpressions;

namespace Marketplace.ValueObjects.Validators;

public class EmailValidator : IValidator<string>
{
    public const int MAX_LENGTH = 100;
    //проверка наличия @ и точки
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);

        if (!EmailRegex.IsMatch(value))
            throw new InvalidEmailFormatException(value);
    }
}