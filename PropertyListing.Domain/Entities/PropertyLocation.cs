using PropertyListing.Domain.Common;

namespace PropertyListing.Domain.Entities;

// Bu sınıf owned type olmalı, ayrı bir entity değil
public class PropertyLocation
{
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Bu navigation property'i kaldırmalıyız çünkü owned type olacak
    // public Property Property { get; set; } = null!;
    // public Guid PropertyId { get; set; }

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