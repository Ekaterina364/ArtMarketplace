using Marketplace.Domain.Entities;

namespace Marketplace.Domain.Exceptions;

public class DownloadNotAllowedException(Purchase purchase)
    : InvalidOperationException($"Download not allowed for purchase {purchase.Id}")
{
    public Purchase PurchaseId => purchase;
}