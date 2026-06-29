using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Implementations.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Loads;

public class TruckAvailabilityRepository
    : RepositoryAsync<TruckAvailability, Guid>, ITruckAvailabilityRepository
{
    public TruckAvailabilityRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TruckAvailability>> GetDriverListingsAsync(string driverId)
    {
        return await _dbSet
            .Where(t => t.DriverId == driverId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TruckAvailability>> GetAvailableListingsAsync()
    {
        return await _dbSet
            .Where(t => t.Status == TruckAvailabilityStatus.Available && t.AvailableUntil > DateTime.UtcNow)
            .OrderBy(t => t.AvailableFrom)
            .ToListAsync();
    }

    public async Task<IEnumerable<TruckAvailability>> GetListingsByStatusAsync(TruckAvailabilityStatus status)
    {
        return await _dbSet
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TruckAvailability>> GetMatchingListingsAsync(double weightKg, double linearMeters, string? location = null)
    {
        var query = _dbSet
            .Where(t => t.Status == TruckAvailabilityStatus.Available
                        && t.AvailableWeightKg >= weightKg
                        && t.AvailableLinearMeters >= linearMeters
                        && t.AvailableUntil > DateTime.UtcNow);

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(t => t.CurrentLocation.Contains(location) || t.Destination != null && t.Destination.Contains(location));
        }

        return await query.OrderBy(t => t.AvailableFrom).ToListAsync();
    }

    public async Task<int> GetAvailableCountAsync()
    {
        return await _dbSet
            .CountAsync(t => t.Status == TruckAvailabilityStatus.Available && t.AvailableUntil > DateTime.UtcNow);
    }
}