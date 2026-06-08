using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Validators;

namespace Marketplace.ValueObjects;

public class CategoryDescription(string? value) : ValueObject<string?>(new CategoryDescriptionValidator(), value);