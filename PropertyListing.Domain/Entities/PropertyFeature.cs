using PropertyListing.Domain.Common;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Domain.Entities;

/// <summary>
/// Emlağın özelliklerini (havuz, asansör vb.) temsil eden entity
/// Entity representing property features (pool, elevator, etc.)
/// </summary>
public class PropertyFeature : BaseAuditableEntity
{
    /// <summary>
    /// Özelliğin adı
    /// Feature name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Özelliğin açıklaması
    /// Feature description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// İlanın ID'si
    /// Property listing ID
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// İlişkili emlak ilanı
    /// Related property listing
    /// </summary>
    public virtual Property Property { get; set; }
} 