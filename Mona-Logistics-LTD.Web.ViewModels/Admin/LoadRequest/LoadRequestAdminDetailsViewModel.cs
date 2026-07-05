using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Web.ViewModels.Admin.Offer;


namespace Mona_Logistics_LTD.Web.ViewModels.Admin.LoadRequest;

public class LoadRequestAdminDetailsViewModel
{
    public Guid Id { get; set; }
    public string CargoName { get; set; } = null!;
    public string CargoDescription { get; set; } = null!;
    public double WeightKg { get; set; }
    public double LinearMeters { get; set; }
    public bool IsFragile { get; set; }
    public bool RequiresCooling { get; set; }
    public string PickupAddress { get; set; } = null!;
    public string DeliveryAddress { get; set; } = null!;
    public DateTime PreferredPickupDate { get; set; }
    public DateTime? PreferredDeliveryDate { get; set; }
    public VehicleType PreferredVehicleType { get; set; }
    public double MinCargoSpaceM3 { get; set; }
    public double MaxWeightCapacityKg { get; set; }
    public string? Notes { get; set; }
    public LoadRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    // Client info
    public string ClientId { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string ClientEmail { get; set; } = null!;
    public string? ClientPhone { get; set; }

    // Offers
    public List<OfferAdminListViewModel> Offers { get; set; } = new();
    public bool CanCreateOffer => Status == LoadRequestStatus.Pending || Status == LoadRequestStatus.UnderReview;
    public bool HasPendingOffer => Offers.Any(o => o.Status == OfferStatus.Pending);
}