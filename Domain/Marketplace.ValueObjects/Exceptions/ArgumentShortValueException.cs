namespace Marketplace.ValueObjects.Exceptions;

public class ArgumentShortValueException(string paramName, string value, int minLength)
    : FormatException($"The \"{paramName}\" length '{value}' is less than minimum allowed length {minLength}.")
{
    public string Value => value;
    public int MinLength => minLength;
}