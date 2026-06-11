
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Data.Models.Base;

namespace Mona_Logistics_LTD.Web.ViewComponents;

public class AccountMenuViewComponent : ViewComponent
{
    private readonly SignInManager<AppUser> _signInManager;

    public AccountMenuViewComponent(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public IViewComponentResult Invoke()
    {
        var isLoggedIn = _signInManager.IsSignedIn(HttpContext.User);
        return View(isLoggedIn);
    }
}