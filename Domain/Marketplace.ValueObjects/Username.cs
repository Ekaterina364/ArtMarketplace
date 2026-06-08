using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Validators;

namespace Marketplace.ValueObjects;
public class Username(string value) : ValueObject<string>(new UsernameValidator(), value);