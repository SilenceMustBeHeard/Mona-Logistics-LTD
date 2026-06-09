using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Services.User.Interfaces.Account;
using Mona_Logistics_LTD.Services.Admin.Interfaces.Message;
using Mona_Logistics_LTD.Web.ViewModels.Admin.Messages;


namespace Mona_Logistics_LTD.Web.Areas.Admin.Controllers.Account;


[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminProfileController : Controller
{
    private readonly IProfileService _profileService;
    private readonly UserManager<AppUser> _userManager;

    private readonly ISystemMessageAdminService _systemInboxMessageService;
    private readonly IContactMessageAdminService _contactMessageService;

    public AdminProfileController(
        ISystemMessageAdminService systemInboxMessageService,
        IProfileService profileService,
        UserManager<AppUser> userManager,

        IContactMessageAdminService contactMessageService)
    {
        _systemInboxMessageService = systemInboxMessageService;
        _profileService = profileService;
        _userManager = userManager;

        _contactMessageService = contactMessageService;
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
        if (model == null)
        {
            TempData["Error"] = "Unable to load profile. Please try again later.";
            return RedirectToAction("Index", "Home");
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> SystemInbox()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["Error"] = "You must be logged in to perform this action.";
            return RedirectToAction("Login", "Account");
        }

        var messages = await _systemInboxMessageService.GetAdminMessagesAsync(user.Id);

        if (messages == null)
        {
            TempData["Error"] = "Unable to load system inbox messages. Please try again later.";
            return RedirectToAction("Index", "Home");
        }

        return View(messages);
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

        var viewModel = await _systemInboxMessageService.GetMessageDetailsAsync(id, user.Id);

        if (viewModel == null)
        {
            TempData["Error"] = "Message not found or you do not have permission to view it.";
            return NotFound();
        }

        return View("SystemMessageDetails", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> AdminInbox()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["Error"] = "You must be logged in to perform this action.";
            return RedirectToAction("Login", "Account");
        }

        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        var isManager = await _userManager.IsInRoleAsync(user, "Manager");

        if (!isAdmin && !isManager)
        {
            TempData["Error"] = "You don't have permission to access this page.";
            return RedirectToAction("Index", "Home");
        }

        var userId = _userManager.GetUserId(User);

        if (userId == null)
        {
            TempData["Error"] = "You must be logged in to perform this action.";
            return RedirectToAction("Login", "Account");
        }

        var contactMessages = await _contactMessageService.GetAdminMessagesAsync(userId);

        var model = new AdminInboxViewModel
        {
            ContactMessages = contactMessages
        };

        return View(model);
    }
}