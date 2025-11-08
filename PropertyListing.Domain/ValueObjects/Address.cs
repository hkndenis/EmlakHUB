namespace PropertyListing.Domain.ValueObjects;

/// <summary>
/// Adres bilgilerini ve coğrafi koordinatları temsil eden value object
/// Value object representing address information and geographic coordinates
/// </summary>
public record Address
{
    /// <summary>
    /// Sokak/cadde adı
    /// Street name
    /// </summary>
    public string Street { get; init; }
    
    /// <summary>
    /// İlçe adı
    /// District name
    /// </summary>
    public string District { get; init; }
    
    /// <summary>
    /// Şehir adı
    /// City name
    /// </summary>
    public string City { get; init; }
    
    /// <summary>
    /// Posta kodu
    /// Postal code
    /// </summary>
    public string PostalCode { get; init; }
    
    /// <summary>
    /// Enlem koordinatı
    /// Latitude coordinate
    /// </summary>
    public double Latitude { get; init; }
    
    /// <summary>
    /// Boylam koordinatı
    /// Longitude coordinate
    /// </summary>
    public double Longitude { get; init; }

    /// <summary>
    /// Yeni bir Address örneği oluşturur
    /// Creates a new Address instance
    /// </summary>
    /// <param name="street">Sokak/cadde adı / Street name</param>
    /// <param name="district">İlçe adı / District name</param>
    /// <param name="city">Şehir adı / City name</param>
    /// <param name="postalCode">Posta kodu / Postal code</param>
    /// <param name="latitude">Enlem koordinatı / Latitude coordinate</param>
    /// <param name="longitude">Boylam koordinatı / Longitude coordinate</param>
    public Address(string street, string district, string city, string postalCode, double latitude, double longitude)
    {
        Street = street;
        District = district;
        City = city;
        PostalCode = postalCode;
        Latitude = latitude;
        Longitude = longitude;
    }
} 