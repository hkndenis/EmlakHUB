using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Properties.Queries.Dtos;

namespace PropertyListing.Application.Properties.Queries.CompareProperties;

public record ComparePropertiesQuery : IRequest<Result<ComparisonDto>>
{
    public List<Guid> PropertyIds { get; init; } = new();
} 