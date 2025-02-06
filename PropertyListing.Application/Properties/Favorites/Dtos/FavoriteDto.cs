using PropertyListing.Application.Common.Mappings;
using PropertyListing.Application.Properties.Queries.Dtos;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Application.Properties.Favorites.Dtos;

public class FavoriteDto : IMapFrom<Favorite>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public PropertyDto Property { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
} 