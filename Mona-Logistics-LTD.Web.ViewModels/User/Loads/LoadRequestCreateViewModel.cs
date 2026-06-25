using System.ComponentModel.DataAnnotations;
using Mona_Logistics_LTD.Data.Common.Enums;

namespace Mona_Logistics_LTD.Web.ViewModels.User.Loads;

public class LoadRequestCreateViewModel
{
    //  Cargo Info
    [Required(ErrorMessage = "Cargo name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Cargo name must be between 3 and 100 characters")]
    public string CargoName { get; set; } = null!;

    [Required(ErrorMessage = "Cargo description is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
    public string CargoDescription { get; set; } = null!;

    [Range(0.1, 100000, ErrorMessage = "Weight must be between 0.1 and 100,000 kg")]
    public double WeightKg { get; set; }

    [Range(0.01, 1000, ErrorMessage = "Volume must be between 0.01 and 1,000 m³")]
    public double VolumeM3 { get; set; }

    public bool IsFragile { get; set; }
    public bool RequiresCooling { get; set; }

    // Transport Details
    [Required(ErrorMessage = "Pickup address is required")]
    [StringLength(200, ErrorMessage = "Pickup address must be less than 200 characters")]
    public string PickupAddress { get; set; } = null!;

    [Required(ErrorMessage = "Delivery address is required")]
    [StringLength(200, ErrorMessage = "Delivery address must be less than 200 characters")]
    public string DeliveryAddress { get; set; } = null!;

    [Required(ErrorMessage = "Preferred pickup date is required")]
    [DataType(DataType.DateTime)]
    public DateTime PreferredPickupDate { get; set; } = DateTime.Today.AddDays(3);

    [DataType(DataType.DateTime)]
    public DateTime? PreferredDeliveryDate { get; set; }

    // Vehicle Requirements
    public VehicleType PreferredVehicleType { get; set; }

    [Range(0, 1000, ErrorMessage = "Min cargo space must be between 0 and 1000 m³")]
    public double MinCargoSpaceM3 { get; set; }

    [Range(0, 100000, ErrorMessage = "Max weight capacity must be between 0 and 100,000 kg")]
    public double MaxWeightCapacityKg { get; set; }

    [StringLength(500, ErrorMessage = "Notes must be less than 500 characters")]
    public string? Notes { get; set; }

    // Preview/Review step
    public bool IsReviewMode { get; set; }

    // Display-only properties for review
    public string VehicleTypeDisplay => PreferredVehicleType switch
    {
        VehicleType.StandardTruck => "Standard Truck",
        VehicleType.RefrigeratedTruck => "Refrigerated Truck",
        VehicleType.ContainerTruck => "Container Truck",
        VehicleType.FlatbedTruck => "Flatbed Truck",
        VehicleType.Van => "Van",
        VehicleType.Trailer => "Trailer",
        _ => "Not specified"
    };
}