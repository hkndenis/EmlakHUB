using PropertyListing.Domain.Common;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Domain.Entities;

public class PropertyImage : BaseAuditableEntity
{
    public Guid PropertyId { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public Property Property { get; set; } = null!;
} 