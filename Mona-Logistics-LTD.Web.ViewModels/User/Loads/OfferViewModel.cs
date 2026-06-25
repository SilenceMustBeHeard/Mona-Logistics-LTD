using Mona_Logistics_LTD.Data.Common.Enums;

namespace Mona_Logistics_LTD.Web.ViewModels.User.Loads;

public class OfferViewModel
{
    public Guid Id { get; set; }
    public Guid LoadRequestId { get; set; }
    public string LoadRequestCargoName { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public decimal FinalPrice => Price - (Discount ?? 0);
    public string? Notes { get; set; }
    public OfferStatus Status { get; set; }
    public DateTime ValidUntil { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;

    public bool CanAccept => Status == OfferStatus.Pending && ValidUntil > DateTime.UtcNow;
    public bool CanReject => Status == OfferStatus.Pending && ValidUntil > DateTime.UtcNow;
    public bool IsExpired => Status == OfferStatus.Pending && ValidUntil <= DateTime.UtcNow;

    public string StatusDisplay => Status switch
    {
        OfferStatus.Pending => "Pending",
        OfferStatus.Accepted => "Accepted",
        OfferStatus.Rejected => "Rejected",
        OfferStatus.Expired => "Expired",
        _ => Status.ToString()
    };

    public string PriceFormatted => $"{FinalPrice:F2} BGN";
    public string ValidUntilFormatted => ValidUntil.ToLocalTime().ToString("dd MMM yyyy HH:mm");
}