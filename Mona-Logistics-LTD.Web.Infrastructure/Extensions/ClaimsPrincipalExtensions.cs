using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Mona_Logistics_LTD.Web.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
        => user.FindFirstValue(ClaimTypes.NameIdentifier);
}
