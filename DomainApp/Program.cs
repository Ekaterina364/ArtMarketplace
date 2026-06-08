using Marketplace.Domain.Entities;
using Marketplace.Domain.Exceptions;
using Marketplace.ValueObjects;

try
{
    var username = new Username("john_artist");
    var email = new Email("artist@example.com");
    var title = new ProductTitle("Awesome Painting");
    var price = new Price(49.99m);
    var fileUrl = new FileUrl("https://marketplace.com/files/painting.png");

    Console.WriteLine($"Username: {username.Value}");
    Console.WriteLine($"Email: {email.Value}");
    Console.WriteLine($"Title: {title.Value}");
    Console.WriteLine($"Price: {price.Value}");
    Console.WriteLine($"FileUrl: {fileUrl.Value}");

    Console.WriteLine("\nСоздаем Username (слишком короткий):");
    var badUsername = new Username("jo");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка валидации: {ex.Message}");
}

var category = new Category(Guid.NewGuid(), new CategoryName("Digital Art"), new CategoryDescription("Paintings and illustrations"));
Console.WriteLine($"\nКатегория: {category.Name}");

var artist = new Artist(Guid.NewGuid(), new Username("painter"), new Email("painter@art.com"));
var buyer = new Buyer(Guid.NewGuid(), new Username("collector"), new Email("collector@example.com"));

Console.WriteLine($"\nАртист: {artist.Username.Value}, email: {artist.Email.Value}");
Console.WriteLine($"Покупатель: {buyer.Username.Value}, email: {buyer.Email.Value}");

var description = new ProductDescription("some description");
var product = artist.CreateProduct(category, new ProductTitle("Title"), description);
Console.WriteLine($"\nСоздан продукт: {product.Title.Value}, статус: {product.Status} (должен быть Draft)");

product.SetPrice(new Price(99.99m));
product.UploadFile(new FileUrl("https://storage.com/sunset.png"));
Console.WriteLine($"После установки цены и файла, статус: {product.Status}");
product.Publish();
Console.WriteLine($"После вызова Publish(), статус: {product.Status} (должен быть Completed)");

var newTitle = new ProductTitle("Sunset Glow");
var newDescription = new ProductDescription("Such a pretty sky.");
bool edited = artist.EditProduct(product, newTitle, newDescription, null, null);
if (edited)
{
    Console.WriteLine($"Продукт отредактирован. Новое название: {product.Title.Value}");
    Console.WriteLine($"Дата последней модификации: {product.LastModifiedAt}");
}

var purchase = buyer.PurchaseProduct(product, product.Price!);
Console.WriteLine($"\nПокупка создана: статус {purchase.Status} (должен быть Pending), цена {purchase.PurchasePrice.Value}");

purchase.MarkAsPaid();
Console.WriteLine($"После оплаты статус покупки: {purchase.Status} (должен быть Paid)");

var download = purchase.CreateDownload();
Console.WriteLine($"Скачивание создано: ID {download.Id}, дата {download.DownloadedAt}");

var secondDownload = purchase.CreateDownload();
Console.WriteLine($"Второе скачивание разрешено: ID {secondDownload.Id}");

artist.DeleteProduct(product);
Console.WriteLine("Продукт удалён своим артистом. У артиста больше нет продуктов: " + (artist.Products.Count==0));