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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
