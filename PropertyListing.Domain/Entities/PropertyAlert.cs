using PropertyListing.Domain.Common;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Domain.Entities;

public class PropertyAlert : BaseAuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? District { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public int? MinBedrooms { get; set; }
    public decimal? MinSquareMeters { get; set; }
    public PropertyType? Type { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime LastNotificationSent { get; set; }

    public ApplicationUser User { get; set; } = null!;
} 