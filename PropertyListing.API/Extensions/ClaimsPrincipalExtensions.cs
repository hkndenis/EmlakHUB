using System.Security.Claims;

namespace PropertyListing.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value;

        return userIdClaim != null 
            ? Guid.Parse(userIdClaim) 
            : Guid.Empty;
    }
} 