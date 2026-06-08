using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Validators;

namespace Marketplace.ValueObjects;
public class CategoryName(string value) : ValueObject<string>(new CategoryNameValidator(), value);