using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Features.Commands.UpdateFeature;

public record UpdatePropertyFeatureCommand : IRequest<Result<Unit>>
{
    public Guid PropertyId { get; init; }
    public Guid FeatureId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
} 