using PropertyListing.Application.Common.Mappings;
using PropertyListing.Application.Properties.Features.Dtos;
using PropertyListing.Domain.Entities;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Application.Properties.Queries.Dtos;

public class PropertyDto : IMapFrom<Property>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public PropertyType Type { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int SquareMeters { get; set; }
    public PropertyStatus Status { get; set; }
    public LocationDto Location { get; set; } = null!;
    public ICollection<PropertyImageDto> Images { get; set; } = new List<PropertyImageDto>();
    public ICollection<PropertyFeatureDto> Features { get; set; } = new List<PropertyFeatureDto>();
    public DateTime CreatedAt { get; set; }
    public int ViewCount { get; set; }
    public DateTime? LastViewedAt { get; set; }
    public double? DistanceInKm { get; set; }
}

public class MoneyDto
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
}

public class AddressDto
{
    public string Street { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class PropertyImageDto
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; }
} 