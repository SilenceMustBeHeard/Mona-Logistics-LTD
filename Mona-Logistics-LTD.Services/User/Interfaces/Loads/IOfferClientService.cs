using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Web.ViewModels.User.Loads;

namespace Mona_Logistics_LTD.Services.User.Interfaces.Loads;

public interface IOfferClientService
{
    Task<IEnumerable<Offer>> GetOffersForRequestAsync(Guid loadRequestId);
    Task<Offer?> GetOfferByIdAsync(Guid id, string clientId);
    Task<bool> AcceptOfferAsync(Guid offerId, string clientId);
    Task<bool> RejectOfferAsync(Guid offerId, string clientId);
    Task<bool> HasActiveOfferAsync(Guid loadRequestId);
}