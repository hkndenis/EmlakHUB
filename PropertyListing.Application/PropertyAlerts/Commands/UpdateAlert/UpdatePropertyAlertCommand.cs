using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Application.PropertyAlerts.Commands.UpdateAlert;

public record UpdatePropertyAlertCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; init; }
    public string City { get; init; } = string.Empty;
    public string? District { get; init; }
    public decimal? MaxPrice { get; init; }
    public decimal? MinPrice { get; init; }
    public int? MinBedrooms { get; init; }
    public decimal? MinSquareMeters { get; init; }
    public PropertyType Type { get; init; }
} 