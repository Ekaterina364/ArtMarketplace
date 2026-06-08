using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Validators;

namespace Marketplace.ValueObjects;
public class ProductTitle(string value) : ValueObject<string>(new ProductTitleValidator(), value);