using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Validators;

namespace Marketplace.ValueObjects;
public class ProductDescription(string? value) : ValueObject<string?>(new ProductDescriptionValidator(), value);