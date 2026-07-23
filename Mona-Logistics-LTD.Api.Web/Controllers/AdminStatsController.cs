using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data;
using Mona_Logistics_LTD.Data.Common.Enums;


namespace Mona_Logistics_LTD.Api.Web.Controllers;

[Route("api/admin")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminStatsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminStatsController(AppDbContext context) qq
    {
        _context = context;
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var totalRequests = await _context.LoadRequests.CountAsync();
        var pendingRequests = await _context.LoadRequests.CountAsync(r => r.Status == LoadRequestStatus.Pending || r.Status == LoadRequestStatus.UnderReview);
        var totalUsers = await _context.Users.CountAsync();
        var activeTrucks = await _context.Trucks.CountAsync(t => t.IsAvailable);
        var newMessages = await _context.ContactMessages.CountAsync(m => !m.IsReadByAdmin);

        return Ok(new
        {
            totalRequests,
            pendingRequests,
            totalUsers,
            activeTrucks,
            newMessages
        });
    }
}