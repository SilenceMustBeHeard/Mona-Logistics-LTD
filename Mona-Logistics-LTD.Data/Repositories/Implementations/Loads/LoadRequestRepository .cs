using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Implementations.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Loads;

public class LoadRequestRepository
    : RepositoryAsync<LoadRequest, Guid>, ILoadRequestRepository
{
    public LoadRequestRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<LoadRequest>> GetClientRequestsAsync(string clientId)
    {
        return await _dbSet
            .Where(r => r.ClientId == clientId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<LoadRequest>> GetPendingRequestsAsync()
    {
        return await _dbSet
            .Where(r => r.Status == LoadRequestStatus.Pending)
            .OrderBy(r => r.PreferredPickupDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<LoadRequest>> GetRequestsByStatusAsync(LoadRequestStatus status)
    {
        return await _dbSet
            .Where(r => r.Status == status)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> GetPendingCountAsync()
    {
        return await _dbSet
            .CountAsync(r => r.Status == LoadRequestStatus.Pending);
    }
}