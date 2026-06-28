using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Base;

namespace Mona_Logistics_LTD.Data.Models.Loads;

public class LoadRequest : BaseDeletableEntity
{
    // Client information
    public string ClientId { get; set; } = null!;

    public virtual AppUser Client { get; set; } = null!;

    // Cargo details
    public string CargoName { get; set; } = null!;

    public string CargoDescription { get; set; } = null!;
    public double WeightKg { get; set; }
    public double LinearMeters { get; set; }
    public bool IsFragile { get; set; }
    public bool RequiresCooling { get; set; }

    // Transport details
    public string PickupAddress { get; set; } = null!;

    public string DeliveryAddress { get; set; } = null!;
    public DateTime PreferredPickupDate { get; set; }
    public DateTime? PreferredDeliveryDate { get; set; }

    // Vehicle requirements
    public VehicleType PreferredVehicleType { get; set; }

    public double MinCargoSpaceM3 { get; set; }
    public double MaxWeightCapacityKg { get; set; }

    // Status and tracking
    public LoadRequestStatus Status { get; set; } = LoadRequestStatus.Pending;

    public string? Notes { get; set; }

    // Offer reference
    public Guid? OfferId { get; set; }

    public virtual Offer? Offer { get; set; }

    // Navigation
    public virtual ICollection<LoadRequestDocument> Documents { get; set; }
        = new HashSet<LoadRequestDocument>();
}