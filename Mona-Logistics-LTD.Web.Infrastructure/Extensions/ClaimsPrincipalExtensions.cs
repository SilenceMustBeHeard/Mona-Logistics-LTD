using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Mona_Logistics_LTD.Web.Infrastructure.Extensions;


public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        
        var userId = user.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        return userId ?? string.Empty;
    }
}