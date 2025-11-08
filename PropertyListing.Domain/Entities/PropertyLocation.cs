using PropertyListing.Domain.Common;

namespace PropertyListing.Domain.Entities;

/// <summary>
/// Emlağın konum bilgilerini temsil eden owned type sınıfı
/// Owned type class representing property location information
/// </summary>
// Bu sınıf owned type olmalı, ayrı bir entity değil
public class PropertyLocation
{
    /// <summary>
    /// Şehir adı
    /// City name
    /// </summary>
    public string City { get; set; } = string.Empty;
    
    /// <summary>
    /// İlçe adı
    /// District name
    /// </summary>
    public string District { get; set; } = string.Empty;
    
    /// <summary>
    /// Posta kodu
    /// Postal code
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Enlem koordinatı
    /// Latitude coordinate
    /// </summary>
    public double Latitude { get; set; }
    
    /// <summary>
    /// Boylam koordinatı
    /// Longitude coordinate
    /// </summary>
    public double Longitude { get; set; }

    // Bu navigation property'i kaldırmalıyız çünkü owned type olacak
    // public Property Property { get; set; } = null!;
    // public Guid PropertyId { get; set; }

    /// <summary>
    /// Address value object'inden PropertyLocation oluşturur
    /// Creates PropertyLocation from Address value object
    /// </summary>
    /// <param name="address">Kaynak adres nesnesi / Source address object</param>
    /// <returns>PropertyLocation nesnesi / PropertyLocation object</returns>
    public static PropertyLocation FromAddress(Domain.ValueObjects.Address address)
    {
        return new PropertyLocation
        {
            City = address.City,
            District = address.District,
            PostalCode = address.PostalCode,
            Latitude = address.Latitude,
            Longitude = address.Longitude
        };
    }
} 