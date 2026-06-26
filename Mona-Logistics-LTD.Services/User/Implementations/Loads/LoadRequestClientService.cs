using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;
using Mona_Logistics_LTD.Services.User.Interfaces.Loads;
using Mona_Logistics_LTD.Web.ViewModels.User.Loads;

namespace Mona_Logistics_LTD.Services.Client.Implementations.Loads;

public class LoadRequestClientService : ILoadRequestClientService
{
    private readonly ILoadRequestRepository _loadRequestRepository;

    public LoadRequestClientService(ILoadRequestRepository loadRequestRepository)
    {
        _loadRequestRepository = loadRequestRepository;
    }

    public async Task<LoadRequest> CreateRequestAsync(LoadRequestCreateViewModel model, string clientId)
    {
        var request = new LoadRequest
        {
            Id = Guid.NewGuid(),
            ClientId = clientId,
            CargoName = model.CargoName,
            CargoDescription = model.CargoDescription,
            WeightKg = model.WeightKg,
            VolumeM3 = model.VolumeM3,
            IsFragile = model.IsFragile,
            RequiresCooling = model.RequiresCooling,
            PickupAddress = model.PickupAddress,
            DeliveryAddress = model.DeliveryAddress,
            PreferredPickupDate = model.PreferredPickupDate.ToUniversalTime(),
            PreferredDeliveryDate = model.PreferredDeliveryDate?.ToUniversalTime(),
            PreferredVehicleType = model.PreferredVehicleType,
            MinCargoSpaceM3 = model.MinCargoSpaceM3,
            MaxWeightCapacityKg = model.MaxWeightCapacityKg,
            Notes = model.Notes,
            Status = LoadRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _loadRequestRepository.AddAsync(request);
        await _loadRequestRepository.SaveChangesAsync();

        return request;
    }

    public async Task<LoadRequest?> GetRequestByIdAsync(Guid id, string clientId)
    {
        return await _loadRequestRepository
            .Query()
            .FirstOrDefaultAsync(r => r.Id == id && r.ClientId == clientId);
    }

    public async Task<IEnumerable<LoadRequest>> GetClientRequestsAsync(string clientId)
    {
        return await _loadRequestRepository.GetClientRequestsAsync(clientId);
    }

    public async Task<bool> UpdateRequestAsync(Guid id, LoadRequestUpdateViewModel model, string clientId)
    {
        var request = await GetRequestByIdAsync(id, clientId);
        if (request == null || !CanEditRequest(request))
            return false;

        request.CargoName = model.CargoName;
        request.CargoDescription = model.CargoDescription;
        request.WeightKg = model.WeightKg;
        request.VolumeM3 = model.VolumeM3;
        request.IsFragile = model.IsFragile;
        request.RequiresCooling = model.RequiresCooling;
        request.PickupAddress = model.PickupAddress;
        request.DeliveryAddress = model.DeliveryAddress;
        request.PreferredPickupDate = model.PreferredPickupDate.ToUniversalTime();
        request.PreferredDeliveryDate = model.PreferredDeliveryDate?.ToUniversalTime();
        request.PreferredVehicleType = model.PreferredVehicleType;
        request.MinCargoSpaceM3 = model.MinCargoSpaceM3;
        request.MaxWeightCapacityKg = model.MaxWeightCapacityKg;
        request.Notes = model.Notes;
        request.UpdatedAt = DateTime.UtcNow;

        await _loadRequestRepository.UpdateAsync(request);
        await _loadRequestRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CancelRequestAsync(Guid id, string clientId)
    {
        var request = await GetRequestByIdAsync(id, clientId);
        if (request == null || request.Status == LoadRequestStatus.Completed || request.Status == LoadRequestStatus.Cancelled)
            return false;

        request.Status = LoadRequestStatus.Cancelled;
        request.UpdatedAt = DateTime.UtcNow;

        await _loadRequestRepository.UpdateAsync(request);
        await _loadRequestRepository.SaveChangesAsync();

        return true;
    }

    public async Task<int> GetPendingCountAsync()
    {
        return await _loadRequestRepository.GetPendingCountAsync();
    }

    public LoadRequestCreateViewModel PreparePreview(LoadRequestCreateViewModel model)
    {
        model.IsReviewMode = true;
        return model;
    }

    public bool CanEditRequest(LoadRequest request)
    {
        return request.Status == LoadRequestStatus.Pending || request.Status == LoadRequestStatus.UnderReview;
    }
}