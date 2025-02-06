using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Domain.Entities;
using PropertyListing.Domain.Enums;
using PropertyListing.Domain.ValueObjects;

namespace PropertyListing.Application.Properties.Commands.CreateProperty;

public record CreatePropertyCommand : IRequest<Result<Guid>>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string Currency { get; init; } = string.Empty;
    public string Street { get; init; } = string.Empty;
    public string District { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public int Bedrooms { get; init; }
    public int Bathrooms { get; init; }
    public decimal SquareMeters { get; init; }
    public PropertyType Type { get; init; }

    public Property ToEntity(ICurrentUserService currentUserService)
    {
        return new Property
        {
            Title = Title,
            Description = Description,
            Price = Money.FromDecimal(Price, Currency),
            Location = new PropertyLocation
            {
                City = City,
                District = District,
                PostalCode = PostalCode,
                Latitude = Latitude,
                Longitude = Longitude
            },
            Bedrooms = Bedrooms,
            Bathrooms = Bathrooms,
            SquareMeters = (int)SquareMeters,
            Type = Type,
            Status = PropertyStatus.Available,
            CreatedBy = currentUserService.UserId.ToString(),
            CreatedAt = DateTime.UtcNow
        };
    }
} 