namespace PropertyListing.Domain.Enums;

/// <summary>
/// Emlak ilanının mevcut durumunu belirtir
/// Specifies the current status of property listing
/// </summary>
public enum PropertyStatus
{
    /// <summary>
    /// İlan aktif ve müsait
    /// Listing is active and available
    /// </summary>
    Available,
    
    /// <summary>
    /// İlan için görüşme devam ediyor
    /// Negotiation in progress for the listing
    /// </summary>
    Pending,
    
    /// <summary>
    /// Emlak satıldı
    /// Property has been sold
    /// </summary>
    Sold,
    
    /// <summary>
    /// Emlak kiralandı
    /// Property has been rented
    /// </summary>
    Rented
} 