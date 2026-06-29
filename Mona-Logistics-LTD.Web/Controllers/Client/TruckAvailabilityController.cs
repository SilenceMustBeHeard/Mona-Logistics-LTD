using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Data.Models.Base;

using Mona_Logistics_LTD.Web.ViewModels.Client.TruckAvailability;
using Mona_Logistics_LTD.Web.ViewModels.User.TruckAvailability;
using Mona_Logistics_LTD.Services.User.Interfaces.Loads;
using Mona_Logistics_LTD.Services.User.Interfaces.Trucks;


namespace Mona_Logistics_LTD.Web.Controllers.Client;

[Authorize]
public class TruckAvailabilityController : Controller
{
    private readonly ITruckAvailabilityClientService _truckAvailabilityService;
    private readonly ITruckService _truckService;
    private readonly UserManager<AppUser> _userManager;

    public TruckAvailabilityController(
        ITruckAvailabilityClientService truckAvailabilityService,
        ITruckService truckService,

        UserManager<AppUser> userManager)
    {
        _truckAvailabilityService = truckAvailabilityService;
        _truckService = truckService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var trucks = await _truckService.GetAllAsync();

        var model = new TruckAvailabilityCreateViewModel
        {
            AvailableFrom = DateTime.Today.AddDays(1),
            AvailableUntil = DateTime.Today.AddDays(7)
        };

        ViewBag.Trucks = trucks;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TruckAvailabilityCreateViewModel model, string action)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        if (action == "preview")
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Trucks = await _truckService.GetAllAsync();
                return View(model);
            }

            model.IsReviewMode = true;
            ViewBag.Trucks = await _truckService.GetAllAsync();
            return View("Review", model);
        }

        if (action == "submit")
        {
            ModelState.Remove("IsReviewMode");
            if (!ModelState.IsValid)
            {
                ViewBag.Trucks = await _truckService.GetAllAsync();
                return View(model);
            }

            var driverId = user.Id; await _truckAvailabilityService.CreateListingAsync(model, driverId);

            TempData["Success"] = "Your truck listing has been created successfully!";
            return RedirectToAction(nameof(MyListings));
        }

        ViewBag.Trucks = await _truckService.GetAllAsync();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> MyListings()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var listings = await _truckAvailabilityService.GetDriverListingsAsync(user.Id);
        return View(listings);
    }

    [HttpGet]
    public async Task<IActionResult> Browse()
    {
        var listings = await _truckAvailabilityService.GetAvailableListingsAsync();
        return View(listings);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var listing = await _truckAvailabilityService.GetListingByIdAsync(id, user.Id);
        if (listing == null)
            return NotFound();

        return View(listing);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var listing = await _truckAvailabilityService.GetListingByIdAsync(id, user.Id);
        if (listing == null || !_truckAvailabilityService.CanEditListing(listing))
            return NotFound();

        var model = new TruckAvailabilityUpdateViewModel
        {
            TruckId = listing.TruckId,
            AvailableWeightKg = listing.AvailableWeightKg,
            AvailableLinearMeters = listing.AvailableLinearMeters,
            AvailableVolumeM3 = listing.AvailableVolumeM3,
            CurrentLocation = listing.CurrentLocation,
            Destination = listing.Destination,
            AvailableFrom = listing.AvailableFrom,
            AvailableUntil = listing.AvailableUntil,
            VehicleType = listing.VehicleType,
            HasCooling = listing.HasCooling,
            HasTailLift = listing.HasTailLift,
            Notes = listing.Notes
        };

        ViewBag.ListingId = id;
        ViewBag.Trucks = await _truckService.GetAllAsync();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TruckAvailabilityUpdateViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        if (!ModelState.IsValid)
        {
            ViewBag.ListingId = id;
            ViewBag.Trucks = await _truckService.GetAllAsync();
            return View(model);
        }

        var success = await _truckAvailabilityService.UpdateListingAsync(id, model, user.Id);
        if (!success)
        {
            TempData["Error"] = "Unable to update listing. It may have been matched or doesn't exist.";
            return RedirectToAction(nameof(MyListings));
        }

        TempData["Success"] = "Listing updated successfully!";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var success = await _truckAvailabilityService.CancelListingAsync(id, user.Id);
        if (!success)
        {
            TempData["Error"] = "Unable to cancel listing.";
            return RedirectToAction(nameof(MyListings));
        }

        TempData["Success"] = "Listing cancelled successfully!";
        return RedirectToAction(nameof(MyListings));
    }
}