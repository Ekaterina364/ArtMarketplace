using Marketplace.ValueObjects.Base;
using Marketplace.ValueObjects.Validators;

namespace Marketplace.ValueObjects;
public class FileUrl(string value) : ValueObject<string>(new FileUrlValidator(), value);