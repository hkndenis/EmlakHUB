using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Properties.Favorites.Dtos;

namespace PropertyListing.Application.Properties.Favorites.Queries.GetFavorites;

public record GetFavoritesQuery(Guid UserId) : IRequest<Result<List<FavoriteDto>>>; 