using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Services.Admin.Interfaces.Loads;
using Mona_Logistics_LTD.Web.Areas.Admin.Controllers.Account;
using Mona_Logistics_LTD.Web.ViewModels.Admin.Offer;

namespace Mona_Logistics_LTD.Web.Areas.Admin.Controllers.LoadRequest;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class LoadRequestAdminController : BaseAdminController
{
    private readonly ILoadRequestAdminService _loadRequestAdminService;

    public LoadRequestAdminController(
        ILoadRequestAdminService loadRequestAdminService,
        UserManager<AppUser> userManager) : base(userManager)
    {
        _loadRequestAdminService = loadRequestAdminService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var requests = await _loadRequestAdminService.GetAllRequestsAsync();
        var newCount = await _loadRequestAdminService.GetNewRequestsCountAsync();

        ViewBag.NewCount = newCount;
        return View(requests);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var request = await _loadRequestAdminService.GetRequestDetailsAsync(id);
        if (request == null)
            return NotFound();

        // Mark as reviewed if it's new
        if (request.Status == LoadRequestStatus.Pending)
        {
            await _loadRequestAdminService.MarkRequestAsReviewedAsync(id);
        }

        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> CreateOffer(Guid id)
    {
        var request = await _loadRequestAdminService.GetRequestDetailsAsync(id);
        if (request == null || !request.CanCreateOffer)
        {
            TempData["Error"] = "Cannot create offer for this request.";
            return RedirectToAction(nameof(Details), new { id });
        }

        var model = new CreateOfferViewModel
        {
            LoadRequestId = id,
            ValidUntil = DateTime.UtcNow.AddDays(7)
        };

        ViewBag.CargoName = request.CargoName;
        ViewBag.ClientName = request.ClientName;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateOffer(CreateOfferViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var request = await _loadRequestAdminService.GetRequestDetailsAsync(model.LoadRequestId);
            ViewBag.CargoName = request?.CargoName ?? "Unknown";
            ViewBag.ClientName = request?.ClientName ?? "Unknown";
            return View(model);
        }

        try
        {
            var adminId = GetUserId().ToString();
            var offer = await _loadRequestAdminService.CreateOfferAsync(model, adminId);

            TempData["Success"] = "Offer created successfully!";
            SetToastMessage("Offer created successfully!", "success");

            return RedirectToAction(nameof(Details), new { id = model.LoadRequestId });
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AcceptOffer(Guid id, Guid requestId)
    {
        var success = await _loadRequestAdminService.AcceptOfferAsync(id);
        if (!success)
        {
            TempData["Error"] = "Unable to accept offer.";
            return RedirectToAction(nameof(Details), new { id = requestId });
        }

        TempData["Success"] = "Offer accepted successfully!";
        SetToastMessage("Offer accepted successfully!", "success");
        return RedirectToAction(nameof(Details), new { id = requestId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RejectOffer(Guid id, Guid requestId)
    {
        var success = await _loadRequestAdminService.RejectOfferAsync(id);
        if (!success)
        {
            TempData["Error"] = "Unable to reject offer.";
            return RedirectToAction(nameof(Details), new { id = requestId });
        }

        TempData["Info"] = "Offer rejected.";
        return RedirectToAction(nameof(Details), new { id = requestId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(Guid id, LoadRequestStatus status)
    {
        var success = await _loadRequestAdminService.UpdateRequestStatusAsync(id, status);
        if (!success)
        {
            TempData["Error"] = "Unable to update status.";
            return RedirectToAction(nameof(Details), new { id });
        }

        TempData["Success"] = $"Status updated to {status}.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetNewCount()
    {
        var count = await _loadRequestAdminService.GetNewRequestsCountAsync();
        return Json(new { count });
    }
}