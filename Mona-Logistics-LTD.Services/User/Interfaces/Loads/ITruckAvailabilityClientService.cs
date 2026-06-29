using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Web.ViewModels.Client.TruckAvailability;
using Mona_Logistics_LTD.Web.ViewModels.User.TruckAvailability;

namespace Mona_Logistics_LTD.Services.User.Interfaces.Loads;

public interface ITruckAvailabilityClientService
{
    Task<TruckAvailability> CreateListingAsync(TruckAvailabilityCreateViewModel model, string driverId);
    Task<TruckAvailability?> GetListingByIdAsync(Guid id, string driverId);
    Task<IEnumerable<TruckAvailability>> GetDriverListingsAsync(string driverId);
    Task<IEnumerable<TruckAvailability>> GetAvailableListingsAsync();
    Task<IEnumerable<TruckAvailability>> GetMatchingListingsAsync(double weightKg, double linearMeters, string? location = null);
    Task<bool> UpdateListingAsync(Guid id, TruckAvailabilityUpdateViewModel model, string driverId);
    Task<bool> CancelListingAsync(Guid id, string driverId);
    Task<bool> MarkAsMatchedAsync(Guid id, Guid loadRequestId);
    Task<int> GetAvailableCountAsync();
    bool CanEditListing(TruckAvailability listing);
}