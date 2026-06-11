
using Microsoft.AspNetCore.Mvc;
using Mona_Logistics_LTD.Services.User.Interfaces.Message;
using System.Security.Claims;


namespace Mona_Logistics_LTD.Web.ViewComponents;

public class UnreadMessageBadgeViewComponent : ViewComponent
{
    private readonly IContactMessageClientService _service;

    public UnreadMessageBadgeViewComponent(IContactMessageClientService service)
    {
        _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var principal = HttpContext.User;

        if (!(principal?.Identity?.IsAuthenticated ?? false))
        {
            return View(0);
        }

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return View(0);
        }

        try
        {
            var count = await _service.GetUserUnreadResponsesCountAsync(userId);
            return View(count);
        }
        catch
        {
            return View(0);
        }
    }

}