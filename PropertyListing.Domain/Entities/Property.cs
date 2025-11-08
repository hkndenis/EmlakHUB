using System;
using System.Collections.Generic;
using PropertyListing.Domain.Common;
using PropertyListing.Domain.Enums;
using PropertyListing.Domain.ValueObjects;

namespace PropertyListing.Domain.Entities;

/// <summary>
/// Emlak ilanını temsil eden ana entity. Tüm ilan bilgilerini içerir.
/// Main entity representing a property listing. Contains all listing information.
/// </summary>
public class Property : BaseAuditableEntity
{
    /// <summary>
    /// Property entity'sinin yapıcı metodu. Koleksiyonları başlatır.
    /// Constructor for Property entity. Initializes collections.
    /// </summary>
    public Property()
    {
        Title = string.Empty;
        Description = string.Empty;
        Images = new List<PropertyImage>();
        Features = new List<PropertyFeature>();
        StatusHistory = new List<PropertyStatusHistory>();
        CreatedBy = string.Empty;
        LastModifiedBy = string.Empty;
    }

    /// <summary>
    /// İlanın başlığı
    /// Title of the listing
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// İlanın detaylı açıklaması
    /// Detailed description of the listing
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// İlanın fiyatı (tutar ve para birimi)
    /// Price of the listing (amount and currency)
    /// </summary>
    public Money Price { get; set; } = null!;
    
    /// <summary>
    /// Emlak tipi (Satılık, Kiralık vb.)
    /// Property type (For Sale, For Rent, etc.)
    /// </summary>
    public PropertyType Type { get; set; }
    
    /// <summary>
    /// İlanın durumu (Aktif, Satıldı, Kiralandı vb.)
    /// Status of the listing (Available, Sold, Rented, etc.)
    /// </summary>
    public PropertyStatus Status { get; set; }
    
    /// <summary>
    /// Yatak odası sayısı
    /// Number of bedrooms
    /// </summary>
    public int Bedrooms { get; set; }
    
    /// <summary>
    /// Banyo sayısı
    /// Number of bathrooms
    /// </summary>
    public int Bathrooms { get; set; }
    
    /// <summary>
    /// Metrekare cinsinden alan
    /// Area in square meters
    /// </summary>
    public int SquareMeters { get; set; }
    
    /// <summary>
    /// İlanın görüntülenme sayısı
    /// Number of times the listing has been viewed
    /// </summary>
    public int ViewCount { get; set; }
    
    /// <summary>
    /// İlanın son görüntülenme tarihi ve saati
    /// Date and time when the listing was last viewed
    /// </summary>
    public DateTime? LastViewedAt { get; set; }
    
    /// <summary>
    /// İlanı oluşturan kullanıcının kimliği
    /// Identifier of the user who created the listing
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    // Navigation Properties
    
    /// <summary>
    /// Emlağın konum bilgileri
    /// Location information of the property
    /// </summary>
    public PropertyLocation Location { get; set; } = null!;
    
    /// <summary>
    /// İlanı oluşturan kullanıcı
    /// User who created the listing
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
    
    /// <summary>
    /// İlana ait görsel dosyalar
    /// Image files of the listing
    /// </summary>
    public ICollection<PropertyImage> Images { get; private set; } = new List<PropertyImage>();
    
    /// <summary>
    /// Emlağın özellikleri (havuz, asansör vb.)
    /// Features of the property (pool, elevator, etc.)
    /// </summary>
    public ICollection<PropertyFeature> Features { get; private set; } = new List<PropertyFeature>();
    
    /// <summary>
    /// İlanın durum değişiklik geçmişi
    /// History of status changes for the listing
    /// </summary>
    public ICollection<PropertyStatusHistory> StatusHistory { get; private set; } = new List<PropertyStatusHistory>();
    
    /// <summary>
    /// Bu ilanı favorilerine ekleyen kullanıcılar
    /// Users who have added this listing to their favorites
    /// </summary>
    public ICollection<Favorite> Favorites { get; private set; } = new List<Favorite>();
}