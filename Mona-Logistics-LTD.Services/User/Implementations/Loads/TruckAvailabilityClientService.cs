using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;
using Mona_Logistics_LTD.Services.User.Interfaces.Loads;
using Mona_Logistics_LTD.Web.ViewModels.User.TruckAvailability;

namespace Mona_Logistics_LTD.Services.User.Implementations.Loads;

public class TruckAvailabilityClientService : ITruckAvailabilityClientService
{
    private readonly ITruckAvailabilityRepository _truckAvailabilityRepository;
    private readonly ILoadRequestRepository _loadRequestRepository;

    public TruckAvailabilityClientService(
        ITruckAvailabilityRepository truckAvailabilityRepository,
        ILoadRequestRepository loadRequestRepository)
    {
        _truckAvailabilityRepository = truckAvailabilityRepository;
        _loadRequestRepository = loadRequestRepository;
    }

    public async Task<TruckAvailability> CreateListingAsync(TruckAvailabilityCreateViewModel model, string driverId)
    {
        var listing = new TruckAvailability
        {
            Id = Guid.NewGuid(),
            DriverId = driverId,
            TruckId = model.TruckId,
            AvailableWeightKg = model.AvailableWeightKg,
            AvailableLinearMeters = model.AvailableLinearMeters,
            AvailableVolumeM3 = model.AvailableVolumeM3,
            CurrentLocation = model.CurrentLocation,
            Destination = model.Destination,
            AvailableFrom = model.AvailableFrom.ToUniversalTime(),
            AvailableUntil = model.AvailableUntil?.ToUniversalTime(),
            VehicleType = model.VehicleType,
            HasCooling = model.HasCooling,
            HasTailLift = model.HasTailLift,
            Status = TruckAvailabilityStatus.Available,
            Notes = model.Notes,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _truckAvailabilityRepository.AddAsync(listing);
        await _truckAvailabilityRepository.SaveChangesAsync();

        return listing;
    }

    public async Task<TruckAvailability?> GetListingByIdAsync(Guid id, string driverId)
    {
        return await _truckAvailabilityRepository
            .Query()
            .Include(t => t.Truck)
            .FirstOrDefaultAsync(t => t.Id == id && t.DriverId == driverId);
    }

    public async Task<IEnumerable<TruckAvailability>> GetDriverListingsAsync(string driverId)
    {
        return await _truckAvailabilityRepository.GetDriverListingsAsync(driverId);
    }

    public async Task<IEnumerable<TruckAvailability>> GetAvailableListingsAsync()
    {
        return await _truckAvailabilityRepository.GetAvailableListingsAsync();
    }

    public async Task<IEnumerable<TruckAvailability>> GetMatchingListingsAsync(double weightKg, double linearMeters, string? location = null)
    {
        return await _truckAvailabilityRepository.GetMatchingListingsAsync(weightKg, linearMeters, location);
    }

    public async Task<bool> UpdateListingAsync(Guid id, TruckAvailabilityUpdateViewModel model, string driverId)
    {
        var listing = await GetListingByIdAsync(id, driverId);
        if (listing == null || !CanEditListing(listing))
            return false;

        listing.TruckId = model.TruckId;
        listing.AvailableWeightKg = model.AvailableWeightKg;
        listing.AvailableLinearMeters = model.AvailableLinearMeters;
        listing.AvailableVolumeM3 = model.AvailableVolumeM3;
        listing.CurrentLocation = model.CurrentLocation;
        listing.Destination = model.Destination;
        listing.AvailableFrom = model.AvailableFrom.ToUniversalTime();
        listing.AvailableUntil = model.AvailableUntil?.ToUniversalTime();
        listing.VehicleType = model.VehicleType;
        listing.HasCooling = model.HasCooling;
        listing.HasTailLift = model.HasTailLift;
        listing.Notes = model.Notes;
        listing.UpdatedAt = DateTime.UtcNow;

        await _truckAvailabilityRepository.UpdateAsync(listing);
        await _truckAvailabilityRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CancelListingAsync(Guid id, string driverId)
    {
        var listing = await GetListingByIdAsync(id, driverId);
        if (listing == null || listing.Status == TruckAvailabilityStatus.Matched)
            return false;

        listing.Status = TruckAvailabilityStatus.Cancelled;
        listing.UpdatedAt = DateTime.UtcNow;

        await _truckAvailabilityRepository.UpdateAsync(listing);
        await _truckAvailabilityRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> MarkAsMatchedAsync(Guid id, Guid loadRequestId)
    {
        var listing = await _truckAvailabilityRepository
            .Query()
            .FirstOrDefaultAsync(t => t.Id == id);

        if (listing == null || listing.Status != TruckAvailabilityStatus.Available)
            return false;

        var loadRequest = await _loadRequestRepository
            .Query()
            .FirstOrDefaultAsync(l => l.Id == loadRequestId);

        if (loadRequest == null)
            return false;

        listing.Status = TruckAvailabilityStatus.Matched;
        listing.MatchedLoadRequestId = loadRequestId;
        listing.UpdatedAt = DateTime.UtcNow;

        loadRequest.Status = LoadRequestStatus.Accepted;
        loadRequest.UpdatedAt = DateTime.UtcNow;

        await _truckAvailabilityRepository.UpdateAsync(listing);
        await _truckAvailabilityRepository.SaveChangesAsync();

        return true;
    }

    public async Task<int> GetAvailableCountAsync()
    {
        return await _truckAvailabilityRepository.GetAvailableCountAsync();
    }

    public bool CanEditListing(TruckAvailability listing)
    {
        return listing.Status == TruckAvailabilityStatus.Available || listing.Status == TruckAvailabilityStatus.Reserved;
    }
}