using System;
using System.Collections.Generic;
using PropertyListing.Domain.Common;
using PropertyListing.Domain.Enums;
using PropertyListing.Domain.ValueObjects;

namespace PropertyListing.Domain.Entities;

public class Property : BaseAuditableEntity
{
    public Property()
    {
        Title = string.Empty;
        Description = string.Empty;
        Images = new List<PropertyImage>();
        Features = new List<PropertyFeature>();
        StatusHistory = new List<PropertyStatusHistory>();
        CreatedBy = string.Empty;
        LastModifiedBy = string.Empty;
    }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Money Price { get; set; } = null!;
    public PropertyType Type { get; set; }
    public PropertyStatus Status { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int SquareMeters { get; set; }
    public int ViewCount { get; set; }
    public DateTime? LastViewedAt { get; set; }
    public string UserId { get; set; } = string.Empty;

    // Navigation Properties
    public PropertyLocation Location { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public ICollection<PropertyImage> Images { get; private set; } = new List<PropertyImage>();
    public ICollection<PropertyFeature> Features { get; private set; } = new List<PropertyFeature>();
    public ICollection<PropertyStatusHistory> StatusHistory { get; private set; } = new List<PropertyStatusHistory>();
    public ICollection<Favorite> Favorites { get; private set; } = new List<Favorite>();
}