using PropertyListing.Application.Common.Mappings;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Application.Properties.Features.Dtos;

public class PropertyFeatureDto : IMapFrom<PropertyFeature>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
} 