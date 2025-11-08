using System.Security.Claims;

namespace PropertyListing.API.Extensions;

/// <summary>
/// ClaimsPrincipal için genişletme metodları
/// Extension methods for ClaimsPrincipal
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// JWT token'dan kullanıcı ID'sini alır
    /// Gets user ID from JWT token
    /// </summary>
    /// <param name="user">ClaimsPrincipal nesnesi / ClaimsPrincipal object</param>
    /// <returns>Kullanıcı ID'si veya kullanıcı bulunamazsa Guid.Empty / User ID or Guid.Empty if not found</returns>
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value;

        return userIdClaim != null 
            ? Guid.Parse(userIdClaim) 
            : Guid.Empty;
    }
} 