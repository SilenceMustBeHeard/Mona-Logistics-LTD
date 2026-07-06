using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Web.ViewModels.Admin.LoadRequest;
using Mona_Logistics_LTD.Web.ViewModels.Admin.Offer;

namespace Mona_Logistics_LTD.Services.Admin.Interfaces.Loads;

public interface ILoadRequestAdminService
{
    // Load request management
    Task<IEnumerable<LoadRequestAdminListViewModel>> GetAllRequestsAsync();
    Task<LoadRequestAdminDetailsViewModel?> GetRequestDetailsAsync(Guid id);
    Task<int> GetNewRequestsCountAsync();
    Task<bool> MarkRequestAsReviewedAsync(Guid id);
    Task<bool> UpdateRequestStatusAsync(Guid id, LoadRequestStatus status);

    // Offer management
    Task<Offer> CreateOfferAsync(CreateOfferViewModel model, string adminId);
    Task<IEnumerable<OfferAdminListViewModel>> GetOffersForRequestAsync(Guid loadRequestId);
    Task<bool> AcceptOfferAsync(Guid offerId);
    Task<bool> RejectOfferAsync(Guid offerId);
}