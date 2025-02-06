using PropertyListing.Application.Common.Mappings;
using PropertyListing.Domain.Entities;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Application.Properties.Queries.GetStatusHistory;

public class StatusHistoryDto : IMapFrom<PropertyStatusHistory>
{
    public Guid Id { get; set; }
    public PropertyStatus OldStatus { get; set; }
    public PropertyStatus NewStatus { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
} 