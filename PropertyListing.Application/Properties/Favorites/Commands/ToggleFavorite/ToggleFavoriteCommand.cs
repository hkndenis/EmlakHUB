using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Favorites.Commands.ToggleFavorite;

public record ToggleFavoriteCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; init; }
    public Guid PropertyId { get; init; }
} 