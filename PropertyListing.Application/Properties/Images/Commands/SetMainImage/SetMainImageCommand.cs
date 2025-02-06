using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Images.Commands.SetMainImage;

public record SetMainImageCommand : IRequest<Result<Unit>>
{
    public Guid PropertyId { get; init; }
    public Guid ImageId { get; init; }
} 