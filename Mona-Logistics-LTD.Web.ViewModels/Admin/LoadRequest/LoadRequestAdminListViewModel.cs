using Mona_Logistics_LTD.Data.Common.Enums;

namespace Mona_Logistics_LTD.Web.ViewModels.Admin.LoadRequest;

public class LoadRequestAdminListViewModel
{
    public Guid Id { get; set; }
    public string CargoName { get; set; } = null!;
    public string PickupAddress { get; set; } = null!;
    public string DeliveryAddress { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string ClientEmail { get; set; } = null!;
    public double WeightKg { get; set; }
    public double LinearMeters { get; set; }
    public DateTime PreferredPickupDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public LoadRequestStatus Status { get; set; }
    public bool IsNew { get; set; }
    public bool HasActiveOffer { get; set; }

    public string StatusDisplay => Status switch
    {
        LoadRequestStatus.Pending => "Pending",
        LoadRequestStatus.UnderReview => "Under Review",
        LoadRequestStatus.Offered => "Offer Received",
        LoadRequestStatus.Accepted => "Accepted",
        LoadRequestStatus.InProgress => "In Progress",
        LoadRequestStatus.Completed => "Completed",
        LoadRequestStatus.Cancelled => "Cancelled",
        _ => Status.ToString()
    };

    public string StatusCssClass => Status switch
    {
        LoadRequestStatus.Pending => "status-pending",
        LoadRequestStatus.UnderReview => "status-underreview",
        LoadRequestStatus.Offered => "status-offered",
        LoadRequestStatus.Accepted => "status-accepted",
        LoadRequestStatus.InProgress => "status-inprogress",
        LoadRequestStatus.Completed => "status-completed",
        LoadRequestStatus.Cancelled => "status-cancelled",
        _ => ""
    };
}