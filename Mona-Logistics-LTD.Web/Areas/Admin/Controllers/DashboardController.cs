using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Web.Areas.Admin.Controllers.Account;

namespace Mona_Logistics_LTD.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : BaseAdminController
{
    public DashboardController(UserManager<AppUser> userManager) : base(userManager)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}