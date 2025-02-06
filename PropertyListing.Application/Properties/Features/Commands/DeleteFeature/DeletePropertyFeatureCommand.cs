using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Features.Commands.DeleteFeature;

public record DeletePropertyFeatureCommand : IRequest<Result<Unit>>
{
    public Guid PropertyId { get; init; }
    public Guid FeatureId { get; init; }
} 