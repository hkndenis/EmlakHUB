namespace PropertyListing.Application.Properties.Dtos;

public class PropertyDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Type { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int SquareMeters { get; set; }
    public string Status { get; set; }
    public int ViewCount { get; set; }
    public DateTime? LastViewedAt { get; set; }
    public Guid UserId { get; set; }
    public LocationDto Location { get; set; }
    public List<PropertyImageDto> Images { get; set; }
    public List<PropertyFeatureDto> Features { get; set; }
    public DateTime CreatedAt { get; set; }
} 