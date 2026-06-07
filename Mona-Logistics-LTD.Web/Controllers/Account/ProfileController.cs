using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Services.User.Interfaces.Account;
using Mona_Logistics_LTD.Services.User.Interfaces.Message;

namespace Mona_Logistics_LTD.Web.Controllers.Account;


[Authorize]
public class ProfileController : Controller
{
    private readonly IProfileService _profileService;
    private readonly UserManager<AppUser> _userManager;
    private readonly ISystemMessageClientService _systemMessageClientService;
    private readonly IContactMessageClientService _contactMessageClientService;


    public ProfileController(
        IContactMessageClientService contactMessageClientService,
        ISystemMessageClientService systemMessageClientService,
        IProfileService profileService,
        UserManager<AppUser> userManager
        )

    {
        _contactMessageClientService = contactMessageClientService;
        _systemMessageClientService = systemMessageClientService;
        _profileService = profileService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["Error"] = "You must be logged in to perform this action.";
            return RedirectToAction("Login", "Account");
        }

        var model = await _profileService.GetProfileAsync(user.Id);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> SystemMessageDetails(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["Error"] = "You must be logged in to perform this action.";
            return RedirectToAction("Login", "Account");
        }

        var viewModel = await _.GetMessageDetailsAsync(id, user.Id);

        if (viewModel == null)
        {
            TempData["Error"] = "Message not found or you do not have permission to view it.";
            return NotFound();
        }

        return View("SystemMessageDetails", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ContactMessageDetails(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["Error"] = "You must be logged in to perform this action.";
            return RedirectToAction("Login", "Account");
        }

        await _contactMessageClientService.MarkAsReadAsync(id, user.Id);

        var message = await _contactMessageClientService.GetMessageDetailsAsync(id, user.Id);

        if (message == null)
        {
            TempData["Error"] = "Message not found or you do not have permission to view it.";
            return NotFound();
        }

        return View(message);
    }
}