using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Addresses.Queries.GetCoordinates;

public record GetCoordinatesQuery : IRequest<Result<CoordinatesDto>>
{
    public string Address { get; init; } = string.Empty;
    public string? City { get; init; }
    public string? District { get; init; }
}

public class CoordinatesDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string FormattedAddress { get; set; } = string.Empty;
} 