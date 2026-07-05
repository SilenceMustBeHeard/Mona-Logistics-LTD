using Mona_Logistics_LTD.Data.Common.Enums;

namespace Mona_Logistics_LTD.Web.ViewModels.Admin.Offer;

public class OfferAdminListViewModel
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public decimal FinalPrice => Price - (Discount ?? 0);
    public OfferStatus Status { get; set; }
    public DateTime ValidUntil { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedByName { get; set; } = null!;
    public string? Notes { get; set; }

    public bool IsExpired => Status == OfferStatus.Pending && ValidUntil < DateTime.UtcNow;
    public string StatusDisplay => Status switch
    {
        OfferStatus.Pending => IsExpired ? "Expired" : "Pending",
        OfferStatus.Accepted => "Accepted",
        OfferStatus.Rejected => "Rejected",
        OfferStatus.Expired => "Expired",
        _ => Status.ToString()
    };
}