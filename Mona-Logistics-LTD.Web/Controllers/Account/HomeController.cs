using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Services.Localizer.Interfaces;
using Mona_Logistics_LTD.Web.Models;
using System.Diagnostics;

namespace Mona_Logistics_LTD.Web.Controllers.Account;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    [HttpGet]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        if (!string.IsNullOrEmpty(culture))
        {
            Response.Cookies.Append(".AspNetCore.Culture",
                $"c={culture}|uic={culture}",
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true,
                    HttpOnly = false,  
                    SameSite = SameSiteMode.Lax,
                    Path = "/"
                });
        }

        return LocalRedirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
