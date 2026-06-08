using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Validators;

namespace Marketplace.ValueObjects;
public class Email(string value) : ValueObject<string>(new EmailValidator(), value);