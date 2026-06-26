using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Base;

using Mona_Logistics_LTD.Services.User.Interfaces.Loads;

namespace Mona_Logistics_LTD.Web.Controllers.Client;

[Authorize]
public class OfferController : Controller
{
    private readonly IOfferClientService _offerService;
    private readonly ILoadRequestClientService _loadRequestService;
    private readonly UserManager<AppUser> _userManager;

    public OfferController(
        IOfferClientService offerService,
        ILoadRequestClientService loadRequestService,
        UserManager<AppUser> userManager)
    {
        _offerService = offerService;
        _loadRequestService = loadRequestService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(Guid requestId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var request = await _loadRequestService.GetRequestByIdAsync(requestId, user.Id);
        if (request == null)
            return NotFound();

        var offers = await _offerService.GetOffersForRequestAsync(requestId);

        ViewBag.RequestId = requestId;
        ViewBag.RequestCargoName = request.CargoName;
        ViewBag.CanAcceptOffer = request.Status == LoadRequestStatus.Offered || request.Status == LoadRequestStatus.Pending;

        return View(offers);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Accept(Guid id, Guid requestId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var success = await _offerService.AcceptOfferAsync(id, user.Id);
        if (!success)
        {
            TempData["Error"] = "Unable to accept offer. It may have expired or been already processed.";
            return RedirectToAction(nameof(Index), new { requestId });
        }

        TempData["Success"] = "Offer accepted successfully! Your request is now in progress.";
        return RedirectToAction("Details", "LoadRequest", new { id = requestId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(Guid id, Guid requestId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var success = await _offerService.RejectOfferAsync(id, user.Id);
        if (!success)
        {
            TempData["Error"] = "Unable to reject offer.";
            return RedirectToAction(nameof(Index), new { requestId });
        }

        TempData["Info"] = "Offer rejected successfully.";
        return RedirectToAction(nameof(Index), new { requestId });
    }
}