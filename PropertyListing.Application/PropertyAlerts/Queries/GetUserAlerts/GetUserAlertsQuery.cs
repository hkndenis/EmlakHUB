using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Application.PropertyAlerts.Queries.GetUserAlerts;

public record GetUserAlertsQuery : IRequest<Result<List<PropertyAlertDto>>>;

public class PropertyAlertDto
{
    public Guid Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string? District { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public int? MinBedrooms { get; set; }
    public decimal? MinSquareMeters { get; set; }
    public PropertyType Type { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastNotificationSent { get; set; }
} 