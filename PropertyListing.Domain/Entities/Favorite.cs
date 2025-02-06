using PropertyListing.Domain.Common;

namespace PropertyListing.Domain.Entities;

public class Favorite : BaseAuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public Guid PropertyId { get; set; }
    
    // Navigation properties
    public ApplicationUser User { get; set; } = null!;
    public Property Property { get; set; } = null!;
} 