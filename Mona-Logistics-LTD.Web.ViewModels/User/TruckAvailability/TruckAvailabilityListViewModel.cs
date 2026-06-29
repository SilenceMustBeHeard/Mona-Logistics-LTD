using Mona_Logistics_LTD.Data.Common.Enums;

namespace Mona_Logistics_LTD.Web.ViewModels.Client.TruckAvailability;

public class TruckAvailabilityListViewModel
{
    public Guid Id { get; set; }
    public string CurrentLocation { get; set; } = null!;
    public string? Destination { get; set; }
    public double AvailableWeightKg { get; set; }
    public double AvailableLinearMeters { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime? AvailableUntil { get; set; }
    public TruckAvailabilityStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string TruckInfo { get; set; } = null!;

    public bool CanEdit => Status == TruckAvailabilityStatus.Available || Status == TruckAvailabilityStatus.Reserved;
    public bool CanCancel => Status != TruckAvailabilityStatus.Matched && Status != TruckAvailabilityStatus.Cancelled;

    public string StatusDisplay => Status switch
    {
        TruckAvailabilityStatus.Available => "Available",
        TruckAvailabilityStatus.Reserved => "Reserved",
        TruckAvailabilityStatus.Matched => "Matched",
        TruckAvailabilityStatus.Expired => "Expired",
        TruckAvailabilityStatus.Cancelled => "Cancelled",
        _ => Status.ToString()
    };

    public string StatusCssClass => Status switch
    {
        TruckAvailabilityStatus.Available => "status-available",
        TruckAvailabilityStatus.Reserved => "status-reserved",
        TruckAvailabilityStatus.Matched => "status-matched",
        TruckAvailabilityStatus.Expired => "status-expired",
        TruckAvailabilityStatus.Cancelled => "status-cancelled",
        _ => ""
    };
}