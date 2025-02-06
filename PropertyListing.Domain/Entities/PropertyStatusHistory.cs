using PropertyListing.Domain.Common;
using PropertyListing.Domain.Enums;

namespace PropertyListing.Domain.Entities;

public class PropertyStatusHistory : BaseAuditableEntity
{
    public PropertyStatus Status { get; set; }
    public string Note { get; set; }
    public Guid PropertyId { get; set; }
    public virtual Property Property { get; set; }
} 