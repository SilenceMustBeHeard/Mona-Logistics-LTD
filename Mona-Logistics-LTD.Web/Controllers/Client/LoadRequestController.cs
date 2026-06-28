using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Data.Models.Base;

using Mona_Logistics_LTD.Services.User.Interfaces.Loads;
using Mona_Logistics_LTD.Web.ViewModels.User.Loads;


namespace Mona_Logistics_LTD.Web.Controllers.Client;

[Authorize]
public class LoadRequestController : Controller
{
    private readonly ILoadRequestClientService _loadRequestService;
    private readonly UserManager<AppUser> _userManager;

    public LoadRequestController(
        ILoadRequestClientService loadRequestService,
        UserManager<AppUser> userManager)
    {
        _loadRequestService = loadRequestService;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new LoadRequestCreateViewModel
        {
            PreferredPickupDate = DateTime.Today.AddDays(3)
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LoadRequestCreateViewModel model, string action)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        if (action == "preview")
        {
            if (!ModelState.IsValid)
                return View(model);

            var previewModel = _loadRequestService.PreparePreview(model);
            return View("Review", previewModel);
        }

        if (action == "submit")
        {
            ModelState.Remove("IsReviewMode");
            if (!ModelState.IsValid)
                return View(model);

            await _loadRequestService.CreateRequestAsync(model, user.Id);
            TempData["Success"] = "Your load request has been submitted successfully!";
            return RedirectToAction(nameof(MyRequests));
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> MyRequests()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var requests = await _loadRequestService.GetClientRequestsAsync(user.Id);
        return View(requests);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var request = await _loadRequestService.GetRequestByIdAsync(id, user.Id);
        if (request == null)
            return NotFound();

        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var request = await _loadRequestService.GetRequestByIdAsync(id, user.Id);
        if (request == null || !_loadRequestService.CanEditRequest(request))
            return NotFound();

        var model = new LoadRequestUpdateViewModel
        {
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
            Notes = request.Notes
        };

        ViewBag.RequestId = id;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, LoadRequestUpdateViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        if (!ModelState.IsValid)
        {
            ViewBag.RequestId = id;
            return View(model);
        }

        var success = await _loadRequestService.UpdateRequestAsync(id, model, user.Id);
        if (!success)
        {
            TempData["Error"] = "Unable to update request. It may have been processed or doesn't exist.";
            return RedirectToAction(nameof(MyRequests));
        }

        TempData["Success"] = "Request updated successfully!";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var success = await _loadRequestService.CancelRequestAsync(id, user.Id);
        if (!success)
        {
            TempData["Error"] = "Unable to cancel request.";
            return RedirectToAction(nameof(MyRequests));
        }

        TempData["Success"] = "Request cancelled successfully!";
        return RedirectToAction(nameof(MyRequests));
    }
}