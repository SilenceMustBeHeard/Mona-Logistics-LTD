using Mona_Logistics_LTD.Data.Common.Enums;

namespace Mona_Logistics_LTD.Web.ViewModels.User.Loads;

public class LoadRequestListViewModel
{
    public Guid Id { get; set; }
    public string CargoName { get; set; } = null!;
    public string PickupAddress { get; set; } = null!;
    public string DeliveryAddress { get; set; } = null!;
    public DateTime PreferredPickupDate { get; set; }
    public LoadRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool CanEdit => Status == LoadRequestStatus.Pending;
    public bool CanCancel => Status == LoadRequestStatus.Pending || Status == LoadRequestStatus.UnderReview;

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