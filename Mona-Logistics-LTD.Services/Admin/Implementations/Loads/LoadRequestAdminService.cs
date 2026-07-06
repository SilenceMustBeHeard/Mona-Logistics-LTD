using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;
using Mona_Logistics_LTD.Services.Admin.Interfaces.Loads;
using Mona_Logistics_LTD.Web.ViewModels.Admin.LoadRequest;
using Mona_Logistics_LTD.Web.ViewModels.Admin.Offer;

namespace Mona_Logistics_LTD.Services.Admin.Implementations.Loads;

public class LoadRequestAdminService : ILoadRequestAdminService
{
    private readonly ILoadRequestRepository _loadRequestRepository;
    private readonly IOfferRepository _offerRepository;
    private readonly UserManager<AppUser> _userManager;

    public LoadRequestAdminService(
        ILoadRequestRepository loadRequestRepository,
        IOfferRepository offerRepository,
        UserManager<AppUser> userManager)
    {
        _loadRequestRepository = loadRequestRepository;
        _offerRepository = offerRepository;
        _userManager = userManager;
    }

    public async Task<IEnumerable<LoadRequestAdminListViewModel>> GetAllRequestsAsync()
    {
        var requests = await _loadRequestRepository
            .Query()
            .Include(r => r.Client)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        var result = new List<LoadRequestAdminListViewModel>();

        foreach (var r in requests)
        {
            var hasActiveOffer = await _offerRepository
                .Query()
                .AnyAsync(o => o.LoadRequestId == r.Id && o.Status == OfferStatus.Pending);

            result.Add(new LoadRequestAdminListViewModel
            {
                Id = r.Id,
                CargoName = r.CargoName,
                PickupAddress = r.PickupAddress,
                DeliveryAddress = r.DeliveryAddress,
                ClientName = $"{r.Client.FirstName} {r.Client.LastName}",
                ClientEmail = r.Client.Email ?? string.Empty,
                WeightKg = r.WeightKg,
                LinearMeters = r.LinearMeters,
                PreferredPickupDate = r.PreferredPickupDate,
                CreatedAt = r.CreatedAt,
                Status = r.Status,
                IsNew = r.CreatedAt > DateTime.UtcNow.AddDays(-1), // New = created in last 24h
                HasActiveOffer = hasActiveOffer
            });
        }

        return result;
    }

    public async Task<LoadRequestAdminDetailsViewModel?> GetRequestDetailsAsync(Guid id)
    {
        var request = await _loadRequestRepository
            .Query()
            .Include(r => r.Client)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (request == null)
            return null;

        // Get offers for this request
        var offers = await _offerRepository
            .Query()
            .Include(o => o.CreatedBy)
            .Where(o => o.LoadRequestId == id)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        var offerViewModels = offers.Select(o => new OfferAdminListViewModel
        {
            Id = o.Id,
            Price = o.Price,
            Discount = o.Discount,
            Status = o.Status,
            ValidUntil = o.ValidUntil,
            CreatedAt = o.CreatedAt,
            CreatedByName = $"{o.CreatedBy.FirstName} {o.CreatedBy.LastName}",
            Notes = o.Notes
        }).ToList();

        return new LoadRequestAdminDetailsViewModel
        {
            Id = request.Id,
            CargoName = request.CargoName,
            CargoDescription = request.CargoDescription,
            WeightKg = request.WeightKg,
            LinearMeters = request.LinearMeters,
            IsFragile = request.IsFragile,
            RequiresCooling = request.RequiresCooling,
            PickupAddress = request.PickupAddress,
            DeliveryAddress = request.DeliveryAddress,
            PreferredPickupDate = request.PreferredPickupDate,
            PreferredDeliveryDate = request.PreferredDeliveryDate,
            PreferredVehicleType = request.PreferredVehicleType,
            MinCargoSpaceM3 = request.MinCargoSpaceM3,
            MaxWeightCapacityKg = request.MaxWeightCapacityKg,
            Notes = request.Notes,
            Status = request.Status,
            CreatedAt = request.CreatedAt,
            ClientId = request.ClientId,
            ClientName = $"{request.Client.FirstName} {request.Client.LastName}",
            ClientEmail = request.Client.Email ?? string.Empty,
            ClientPhone = request.Client.PhoneNumber,
            Offers = offerViewModels,
        };
    }

    public async Task<int> GetNewRequestsCountAsync()
    {
        var twentyFourHoursAgo = DateTime.UtcNow.AddDays(-1);
        return await _loadRequestRepository
            .Query()
            .CountAsync(r => r.CreatedAt > twentyFourHoursAgo
                && (r.Status == LoadRequestStatus.Pending || r.Status == LoadRequestStatus.UnderReview));
    }

    public async Task<bool> MarkRequestAsReviewedAsync(Guid id)
    {
        var request = await _loadRequestRepository
            .Query()
            .FirstOrDefaultAsync(r => r.Id == id);

        if (request == null || request.Status != LoadRequestStatus.Pending)
            return false;

        request.Status = LoadRequestStatus.UnderReview;
        request.UpdatedAt = DateTime.UtcNow;

        await _loadRequestRepository.UpdateAsync(request);
        await _loadRequestRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateRequestStatusAsync(Guid id, LoadRequestStatus status)
    {
        var request = await _loadRequestRepository
            .Query()
            .FirstOrDefaultAsync(r => r.Id == id);

        if (request == null)
            return false;

        request.Status = status;
        request.UpdatedAt = DateTime.UtcNow;

        await _loadRequestRepository.UpdateAsync(request);
        await _loadRequestRepository.SaveChangesAsync();

        return true;
    }

    // Offer Management
    public async Task<Offer> CreateOfferAsync(CreateOfferViewModel model, string adminId)
    {
        var request = await _loadRequestRepository
            .Query()
            .FirstOrDefaultAsync(r => r.Id == model.LoadRequestId);

        if (request == null)
            throw new ArgumentException("Load request not found");

        var offer = new Offer
        {
            Id = Guid.NewGuid(),
            LoadRequestId = model.LoadRequestId,
            Price = model.Price,
            Discount = model.Discount,
            Notes = model.Notes,
            ValidUntil = model.ValidUntil.ToUniversalTime(),
            Status = OfferStatus.Pending,
            CreatedById = adminId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        // Update request status to Offered
        if (request.Status == LoadRequestStatus.Pending || request.Status == LoadRequestStatus.UnderReview)
        {
            request.Status = LoadRequestStatus.Offered;
            request.UpdatedAt = DateTime.UtcNow;
        }

        await _offerRepository.AddAsync(offer);
        await _loadRequestRepository.UpdateAsync(request);
        await _loadRequestRepository.SaveChangesAsync();

        return offer;
    }

    public async Task<IEnumerable<OfferAdminListViewModel>> GetOffersForRequestAsync(Guid loadRequestId)
    {
        var offers = await _offerRepository
            .Query()
            .Include(o => o.CreatedBy)
            .Where(o => o.LoadRequestId == loadRequestId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return offers.Select(o => new OfferAdminListViewModel
        {
            Id = o.Id,
            Price = o.Price,
            Discount = o.Discount,
            Status = o.Status,
            ValidUntil = o.ValidUntil,
            CreatedAt = o.CreatedAt,
            CreatedByName = $"{o.CreatedBy.FirstName} {o.CreatedBy.LastName}",
            Notes = o.Notes
        });
    }

    public async Task<bool> AcceptOfferAsync(Guid offerId)
    {
        var offer = await _offerRepository
            .Query()
            .Include(o => o.LoadRequest)
            .FirstOrDefaultAsync(o => o.Id == offerId);

        if (offer == null || offer.Status != OfferStatus.Pending)
            return false;

        offer.Status = OfferStatus.Accepted;
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
        }

        await _offerRepository.UpdateAsync(offer);
        await _offerRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RejectOfferAsync(Guid offerId)
    {
        var offer = await _offerRepository
            .Query()
            .FirstOrDefaultAsync(o => o.Id == offerId);

        if (offer == null || offer.Status != OfferStatus.Pending)
            return false;

        offer.Status = OfferStatus.Rejected;
        offer.UpdatedAt = DateTime.UtcNow;

        await _offerRepository.UpdateAsync(offer);
        await _offerRepository.SaveChangesAsync();

        return true;
    }
}