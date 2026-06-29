using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Models.Trucks;

namespace Mona_Logistics_LTD.Data.Models.Loads;

public class TruckAvailability : BaseDeletableEntity
{
    public string? DriverId { get; set; }
    public virtual Driver? Driver { get; set; }

    public Guid? TruckId { get; set; }
    public virtual Truck? Truck { get; set; }

    public double AvailableWeightKg { get; set; }
    public double AvailableLinearMeters { get; set; }
    public double AvailableVolumeM3 { get; set; }

    public string CurrentLocation { get; set; } = null!;
    public string? Destination { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime? AvailableUntil { get; set; }

    public VehicleType VehicleType { get; set; }
    public bool HasCooling { get; set; }
    public bool HasTailLift { get; set; }

    public TruckAvailabilityStatus Status { get; set; }
        = TruckAvailabilityStatus.Available;

    public string? Notes { get; set; }

    public Guid? MatchedLoadRequestId { get; set; }
    public virtual LoadRequest? MatchedLoadRequest { get; set; }

    public virtual ICollection<TruckAvailabilityDocument> Documents { get; set; }
        = new HashSet<TruckAvailabilityDocument>();
}