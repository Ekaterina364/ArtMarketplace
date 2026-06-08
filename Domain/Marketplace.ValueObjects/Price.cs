using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Validators;

namespace Marketplace.ValueObjects;
public class Price(decimal value) : ValueObject<decimal>(new PriceValidator(), value);