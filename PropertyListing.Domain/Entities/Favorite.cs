using PropertyListing.Domain.Common;

namespace PropertyListing.Domain.Entities;

/// <summary>
/// Kullanıcının favori ilanlarını temsil eden entity
/// Entity representing user's favorite listings
/// </summary>
public class Favorite : BaseAuditableEntity
{
    /// <summary>
    /// Kullanıcı ID'si
    /// User ID
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    
    /// <summary>
    /// İlan ID'si
    /// Property listing ID
    /// </summary>
    public Guid PropertyId { get; set; }
    
    // Navigation properties
    
    /// <summary>
    /// İlişkili kullanıcı
    /// Related user
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
    
    /// <summary>
    /// İlişkili emlak ilanı
    /// Related property listing
    /// </summary>
    public Property Property { get; set; } = null!;
} 