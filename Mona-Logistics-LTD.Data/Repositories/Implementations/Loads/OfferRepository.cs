using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Implementations.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Loads;

public class OfferRepository : RepositoryAsync<Offer, Guid>, IOfferRepository
{
    public OfferRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Offer>> GetOffersForRequestAsync(Guid loadRequestId)
    {
        return await _dbSet
            .Where(o => o.LoadRequestId == loadRequestId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Offer>> GetPendingOffersAsync()
    {
        return await _dbSet
            .Where(o => o.Status == OfferStatus.Pending && o.ValidUntil > DateTime.UtcNow)
            .OrderBy(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Offer>> GetOffersByStatusAsync(OfferStatus status)
    {
        return await _dbSet
            .Where(o => o.Status == status)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<Offer?> GetActiveOfferForRequestAsync(Guid loadRequestId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(o =>
                o.LoadRequestId == loadRequestId &&
                o.Status == OfferStatus.Pending &&
                o.ValidUntil > DateTime.UtcNow);
    }
}