using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Application.PropertyAlerts.Commands.CreateAlert;

public class CreatePropertyAlertCommand : IRequest<Result<Guid>>
{
    public string City { get; set; }
    public string? District { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinBedrooms { get; set; }
    public int? MinSquareMeters { get; set; }
    public PropertyType Type { get; set; }
} 