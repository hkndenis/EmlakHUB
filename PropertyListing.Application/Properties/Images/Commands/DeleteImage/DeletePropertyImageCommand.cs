using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Images.Commands.DeleteImage;

public record DeletePropertyImageCommand : IRequest<Result>
{
    public Guid ImageId { get; init; }
} 