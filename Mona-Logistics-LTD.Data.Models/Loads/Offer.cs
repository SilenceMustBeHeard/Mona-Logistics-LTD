using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Base;

namespace Mona_Logistics_LTD.Data.Models.Loads;

public class Offer : BaseDeletableEntity
{
    public Guid LoadRequestId { get; set; }
    public virtual LoadRequest LoadRequest { get; set; } = null!;

    // optional, if assigning specific truck
    public string? TruckId { get; set; } 

    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public decimal FinalPrice => Price - (Discount ?? 0);

    public string? Notes { get; set; }
    public OfferStatus Status { get; set; } 
        = OfferStatus.Pending;

    public DateTime ValidUntil { get; set; }

    public string CreatedById { get; set; } = null!;
    public virtual AppUser CreatedBy { get; set; } = null!;

    public DateTime? AcceptedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
}