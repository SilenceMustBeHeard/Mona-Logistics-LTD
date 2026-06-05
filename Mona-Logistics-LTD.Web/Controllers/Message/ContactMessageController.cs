using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Services.User.Interfaces.Message;

namespace Mona_Logistics_LTD.Web.Controllers.Message;


public class ContactMessageController : Controller
{
    private readonly IContactMessageClientService _contactMessageService;

    public ContactMessageController(IContactMessageClientService contactMessageService)
    {
        _contactMessageService = contactMessageService;
    }




    [HttpGet]
    public IActionResult Index()
    {
        return View(new ContactMessageCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactMessageCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _contactMessageService.SendContactMessageAsync(model, User);
        TempData["Success"] = "Your message has been sent successfully! We'll get back to you soon.";
        return RedirectToAction("Index", "Home");

    }
}