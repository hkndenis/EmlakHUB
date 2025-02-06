using PropertyListing.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PropertyListing.Domain.Entities;

public class ApplicationUser : IdentityUser<string>
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    
    // Navigation properties
    public ICollection<Property> Properties { get; private set; } = new List<Property>();
    public ICollection<Favorite> Favorites { get; private set; } = new List<Favorite>();
} 