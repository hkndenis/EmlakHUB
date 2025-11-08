using PropertyListing.Domain.Common;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Domain.Entities;

/// <summary>
/// Emlak ilanına ait görsel dosyayı temsil eden entity
/// Entity representing an image file for a property listing
/// </summary>
public class PropertyImage : BaseAuditableEntity
{
    /// <summary>
    /// İlanın ID'si
    /// Property listing ID
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// Görselin URL adresi
    /// URL of the image
    /// </summary>
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// Bu görselin ana görsel olup olmadığı
    /// Whether this is the main image
    /// </summary>
    public bool IsMain { get; set; }
    
    /// <summary>
    /// İlişkili emlak ilanı
    /// Related property listing
    /// </summary>
    public Property Property { get; set; } = null!;
} 