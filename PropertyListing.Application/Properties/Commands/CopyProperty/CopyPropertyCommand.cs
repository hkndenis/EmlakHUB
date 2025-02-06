using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Application.Properties.Commands.CopyProperty;

public record CopyPropertyCommand : IRequest<Result<Guid>>
{
    public Guid SourcePropertyId { get; init; }
    public PropertyType NewType { get; init; }
    public decimal? NewPrice { get; init; }
} 