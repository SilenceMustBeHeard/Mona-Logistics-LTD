using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;

public interface IOfferRepository : IFullRepositoryAsync<Offer, Guid>
{
    Task<IEnumerable<Offer>> GetOffersForRequestAsync(Guid loadRequestId);

    Task<IEnumerable<Offer>> GetPendingOffersAsync();

    Task<IEnumerable<Offer>> GetOffersByStatusAsync(OfferStatus status);

    Task<Offer?> GetActiveOfferForRequestAsync(Guid loadRequestId);
}