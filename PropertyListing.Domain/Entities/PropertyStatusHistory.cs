using PropertyListing.Domain.Common;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Domain.Entities;

/// <summary>
/// İlanın durum değişiklik geçmişini kaydeden entity
/// Entity recording property status change history
/// </summary>
public class PropertyStatusHistory : BaseAuditableEntity
{
    /// <summary>
    /// İlanın yeni durumu
    /// New status of the listing
    /// </summary>
    public PropertyStatus Status { get; set; }
    
    /// <summary>
    /// Durum değişikliği ile ilgili not
    /// Note regarding the status change
    /// </summary>
    public string Note { get; set; }
    
    /// <summary>
    /// İlan ID'si
    /// Property listing ID
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// İlişkili emlak ilanı
    /// Related property listing
    /// </summary>
    public virtual Property Property { get; set; }
} 