using System.ComponentModel.DataAnnotations;
using Mona_Logistics_LTD.Data.Common.Enums;

namespace Mona_Logistics_LTD.Web.ViewModels.User.TruckAvailability;

public class TruckAvailabilityUpdateViewModel
{
    [Required]
    public Guid? TruckId { get; set; }

    [Range(0.1, 100000)]
    public double AvailableWeightKg { get; set; }

    [Range(0.01, 20)]
    public double AvailableLinearMeters { get; set; }

    [Range(0.01, 1000)]
    public double AvailableVolumeM3 { get; set; }

    [Required]
    [StringLength(200)]
    public string CurrentLocation { get; set; } = null!;

    [StringLength(200)]
    public string? Destination { get; set; }

    [Required]
    public DateTime AvailableFrom { get; set; }

    public DateTime? AvailableUntil { get; set; }

    public VehicleType VehicleType { get; set; }
    public bool HasCooling { get; set; }
    public bool HasTailLift { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}