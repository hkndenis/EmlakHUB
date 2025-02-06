using PropertyListing.Domain.Common;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Domain.Entities;

public class PropertyFeature : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid PropertyId { get; set; }
    public virtual Property Property { get; set; }
} 