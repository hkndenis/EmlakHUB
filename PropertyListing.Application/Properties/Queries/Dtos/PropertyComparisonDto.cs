using PropertyListing.Domain.Enums;
using PropertyListing.Application.Properties.Features.Dtos;

namespace PropertyListing.Application.Properties.Queries.Dtos;

public class PropertyComparisonDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int SquareMeters { get; set; }
    public LocationDto Location { get; set; } = null!;
    public PropertyType Type { get; set; }
    public ICollection<PropertyFeatureDto> Features { get; set; } = new List<PropertyFeatureDto>();
} 