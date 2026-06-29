using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;
using Mona_Logistics_LTD.Services.User.Interfaces.Loads;

namespace Mona_Logistics_LTD.Services.User.Implementations.Loads;

public class OfferClientService : IOfferClientService
{
    private readonly IOfferRepository _offerRepository;
    private readonly ILoadRequestRepository _loadRequestRepository;

    public OfferClientService(
        IOfferRepository offerRepository,
        ILoadRequestRepository loadRequestRepository)
    {
        _offerRepository = offerRepository;
        _loadRequestRepository = loadRequestRepository;
    }

    public async Task<IEnumerable<Offer>> GetOffersForRequestAsync(Guid loadRequestId)
    {
        return await _offerRepository.GetOffersForRequestAsync(loadRequestId);
    }

    public async Task<Offer?> GetOfferByIdAsync(Guid id, string clientId)
    {
        return await _offerRepository
            .Query()
            .Include(o => o.LoadRequest)
            .FirstOrDefaultAsync(o => o.Id == id && o.LoadRequest.ClientId == clientId);
    }

    public async Task<bool> AcceptOfferAsync(Guid offerId, string clientId)
    {
        var offer = await _offerRepository
            .Query()
            .Include(o => o.LoadRequest)
            .FirstOrDefaultAsync(o => o.Id == offerId && o.LoadRequest.ClientId == clientId);

        if (offer == null || offer.Status != OfferStatus.Pending)
            return false;

        // Update offer status
        offer.Status = OfferStatus.Accepted;
        offer.AcceptedAt = DateTime.UtcNow;

        // Update request status
        offer.LoadRequest.Status = LoadRequestStatus.Accepted;
        offer.LoadRequest.UpdatedAt = DateTime.UtcNow;

        // Reject all other offers for this request
        var otherOffers = await _offerRepository
            .Query()
            .Where(o => o.LoadRequestId == offer.LoadRequestId && o.Id != offerId)
            .ToListAsync();

        foreach (var other in otherOffers)
        {
            other.Status = OfferStatus.Rejected;
            other.RejectedAt = DateTime.UtcNow;
        }

        await _offerRepository.UpdateAsync(offer);
        await _offerRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RejectOfferAsync(Guid offerId, string clientId)
    {
        var offer = await _offerRepository
            .Query()
            .Include(o => o.LoadRequest)
            .FirstOrDefaultAsync(o => o.Id == offerId && o.LoadRequest.ClientId == clientId);

        if (offer == null || offer.Status != OfferStatus.Pending)
            return false;

        offer.Status = OfferStatus.Rejected;
        offer.RejectedAt = DateTime.UtcNow;

        await _offerRepository.UpdateAsync(offer);
        await _offerRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> HasActiveOfferAsync(Guid loadRequestId)
    {
        return await _offerRepository.GetActiveOfferForRequestAsync(loadRequestId) != null;
    }
}