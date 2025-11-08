using PropertyListing.Domain.Common;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Domain.Entities;

/// <summary>
/// Kullanıcının belirli kriterlere göre ilan bildirimi almak için oluşturduğu alarmı temsil eden entity
/// Entity representing user's alert for receiving notifications based on specific criteria
/// </summary>
public class PropertyAlert : BaseAuditableEntity
{
    /// <summary>
    /// Kullanıcı ID'si
    /// User ID
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    
    /// <summary>
    /// Aranılan şehir
    /// Desired city
    /// </summary>
    public string City { get; set; } = string.Empty;
    
    /// <summary>
    /// Aranılan ilçe (opsiyonel)
    /// Desired district (optional)
    /// </summary>
    public string? District { get; set; }
    
    /// <summary>
    /// Maksimum fiyat limiti
    /// Maximum price limit
    /// </summary>
    public decimal? MaxPrice { get; set; }
    
    /// <summary>
    /// Minimum fiyat limiti
    /// Minimum price limit
    /// </summary>
    public decimal? MinPrice { get; set; }
    
    /// <summary>
    /// Minimum yatak odası sayısı
    /// Minimum number of bedrooms
    /// </summary>
    public int? MinBedrooms { get; set; }
    
    /// <summary>
    /// Minimum metrekare
    /// Minimum square meters
    /// </summary>
    public decimal? MinSquareMeters { get; set; }
    
    /// <summary>
    /// Emlak tipi (Satılık/Kiralık)
    /// Property type (For Sale/For Rent)
    /// </summary>
    public PropertyType? Type { get; set; }
    
    /// <summary>
    /// Alarmın aktif olup olmadığı
    /// Whether the alert is active
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Son bildirimin gönderildiği tarih ve saat
    /// Date and time when last notification was sent
    /// </summary>
    public DateTime LastNotificationSent { get; set; }

    /// <summary>
    /// İlişkili kullanıcı
    /// Related user
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
} 