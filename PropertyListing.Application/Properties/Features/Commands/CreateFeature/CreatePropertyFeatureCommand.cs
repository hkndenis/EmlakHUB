using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Features.Commands.CreateFeature;

public record CreatePropertyFeatureCommand : IRequest<Result<Guid>>
{
    public Guid PropertyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
} 