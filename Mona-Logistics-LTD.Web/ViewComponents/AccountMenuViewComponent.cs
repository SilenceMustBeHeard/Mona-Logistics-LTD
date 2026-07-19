using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Services.Admin.Interfaces.Loads;

namespace Mona_Logistics_LTD.Web.ViewComponents;

public class AccountMenuViewComponent : ViewComponent
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AccountMenuViewComponent(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var isLoggedIn = _signInManager.IsSignedIn(HttpContext.User);

        var model = isLoggedIn;

        if (isLoggedIn)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.Username = user?.FirstName ?? user?.UserName?.Split('@')[0] ?? user?.Email;
            ViewBag.IsAdmin = HttpContext.User.IsInRole("Admin");
            ViewBag.IsManager = HttpContext.User.IsInRole("Manager");

            // Get new requests count for admin
            if (HttpContext.User.IsInRole("Admin"))
            {
                using var scope = ViewContext.HttpContext.RequestServices.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<ILoadRequestAdminService>();
                ViewBag.NewRequestsCount = await service.GetNewRequestsCountAsync();
            }
        }

        return View(model);  
    }
}