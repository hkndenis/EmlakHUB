using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Properties.Queries.Dtos;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Application.Properties.Queries.GetProperties;

public record GetPropertiesQuery : IRequest<Result<List<PropertyDto>>>
{
    public string? SearchTerm { get; init; }
    public string? City { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public int? MinBedrooms { get; init; }
    public decimal? MinSalePrice { get; init; }
    public decimal? MaxSalePrice { get; init; }
    public decimal? MinRentPrice { get; init; }
    public decimal? MaxRentPrice { get; init; }
    public PropertyType? Type { get; init; }
    public PropertyStatus? Status { get; init; }
    public List<PropertyStatus>? Statuses { get; init; }
    public bool OrderByViewCount { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public double? RadiusInKm { get; init; }
    public bool OrderByDistance { get; init; }
} 