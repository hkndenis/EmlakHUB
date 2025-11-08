namespace PropertyListing.Domain.Common;

/// <summary>
/// Tüm domain entityler için temel sınıf. Benzersiz tanımlayıcı sağlar.
/// Base class for all domain entities. Provides unique identifier.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Entity'nin benzersiz tanımlayıcısı
    /// Unique identifier of the entity
    /// </summary>
    public Guid Id { get; set; }
} 