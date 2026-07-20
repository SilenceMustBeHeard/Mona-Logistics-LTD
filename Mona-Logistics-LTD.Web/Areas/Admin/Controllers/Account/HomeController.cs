using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Web.Models;
using System.Diagnostics;

namespace Mona_Logistics_LTD.Web.Areas.Admin.Controllers.Account;

[Area("Admin")]
[Authorize(Roles = "Admin")]

public class HomeController : BaseAdminController
{
    public HomeController(UserManager<AppUser> userManager) : base(userManager)
    {
    }

    public IActionResult Index() => View();


    public IActionResult About() => View();

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}