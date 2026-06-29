using System.ComponentModel.DataAnnotations;
using Mona_Logistics_LTD.Data.Common.Enums;

namespace Mona_Logistics_LTD.Web.ViewModels.Client.TruckAvailability;

public class TruckAvailabilityCreateViewModel
{
    [Required(ErrorMessage = "Truck selection is required")]
    public Guid? TruckId { get; set; }

    [Range(0.1, 100000, ErrorMessage = "Available weight must be between 0.1 and 100,000 kg")]
    public double AvailableWeightKg { get; set; }

    [Range(0.01, 20, ErrorMessage = "Available linear meters must be between 0.01 and 20")]
    public double AvailableLinearMeters { get; set; }

    [Range(0.01, 1000, ErrorMessage = "Available volume must be between 0.01 and 1,000 m³")]
    public double AvailableVolumeM3 { get; set; }

    [Required(ErrorMessage = "Current location is required")]
    [StringLength(200, ErrorMessage = "Current location must be less than 200 characters")]
    public string CurrentLocation { get; set; } = null!;

    [StringLength(200, ErrorMessage = "Destination must be less than 200 characters")]
    public string? Destination { get; set; }

    [Required(ErrorMessage = "Available from date is required")]
    [DataType(DataType.DateTime)]
    public DateTime AvailableFrom { get; set; } = DateTime.Today.AddDays(1);

    [DataType(DataType.DateTime)]
    public DateTime? AvailableUntil { get; set; }

    public VehicleType VehicleType { get; set; }
    public bool HasCooling { get; set; }
    public bool HasTailLift { get; set; }

    [StringLength(500, ErrorMessage = "Notes must be less than 500 characters")]
    public string? Notes { get; set; }

    public bool IsReviewMode { get; set; }

    public string VehicleTypeDisplay => VehicleType switch
    {
        VehicleType.StandardTruck => "Standard Truck",
        VehicleType.ContainerTruck => "Container Truck",
        VehicleType.FlatbedTruck => "Flatbed Truck",
        VehicleType.RefrigeratedTruck => "Refrigerated Truck",
        VehicleType.Van => "Van",
        VehicleType.Trailer => "Trailer",
        _ => "Not specified"
    };


    public string FeaturesDisplay => string.Join(", ", new[]
    {
        HasCooling ? "Cooling" : null,
        HasTailLift ? "Tail Lift" : null
    }.Where(x => x != null));
}