namespace PropertyListing.Domain.Common;

/// <summary>
/// Denetim bilgileri gerektiren entityler için temel sınıf. Oluşturma ve değiştirme bilgilerini içerir.
/// Base class for entities that require audit information. Contains creation and modification tracking.
/// </summary>
public abstract class BaseAuditableEntity : BaseEntity
{
    /// <summary>
    /// Entity'nin oluşturulma tarihi ve saati
    /// Date and time when the entity was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Entity'yi oluşturan kullanıcının kimliği
    /// Identifier of the user who created the entity
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Entity'nin son değiştirilme tarihi ve saati
    /// Date and time when the entity was last modified
    /// </summary>
    public DateTime? LastModifiedAt { get; set; }
    
    /// <summary>
    /// Entity'yi son değiştiren kullanıcının kimliği
    /// Identifier of the user who last modified the entity
    /// </summary>
    public string LastModifiedBy { get; set; } = string.Empty;
} 