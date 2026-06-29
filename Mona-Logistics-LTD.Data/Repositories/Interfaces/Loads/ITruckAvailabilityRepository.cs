using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;

public interface ITruckAvailabilityRepository : IFullRepositoryAsync<TruckAvailability, Guid>
{
    Task<IEnumerable<TruckAvailability>> GetDriverListingsAsync(string driverId);

    Task<IEnumerable<TruckAvailability>> GetAvailableListingsAsync();

    Task<IEnumerable<TruckAvailability>> GetListingsByStatusAsync(TruckAvailabilityStatus status);

    Task<IEnumerable<TruckAvailability>> GetMatchingListingsAsync(double weightKg, double linearMeters, string? location = null);

    Task<int> GetAvailableCountAsync();
}