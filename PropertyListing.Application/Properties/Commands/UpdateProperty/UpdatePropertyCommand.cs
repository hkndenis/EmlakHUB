using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Commands.UpdateProperty;

public record UpdatePropertyCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string Currency { get; init; }
    public string Street { get; init; }
    public string District { get; init; }
    public string City { get; init; }
    public string PostalCode { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public int Bedrooms { get; init; }
    public int Bathrooms { get; init; }
    public decimal SquareMeters { get; init; }
} 