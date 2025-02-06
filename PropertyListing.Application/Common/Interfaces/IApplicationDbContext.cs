using Microsoft.EntityFrameworkCore;
using PropertyListing.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PropertyListing.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Property> Properties { get; }
        DbSet<PropertyFeature> PropertyFeatures { get; }
        DbSet<PropertyImage> PropertyImages { get; }
        DbSet<ApplicationUser> Users { get; }
        DbSet<Favorite> Favorites { get; }
        DbSet<PropertyStatusHistory> PropertyStatusHistory { get; }
        DbSet<PropertyAlert> PropertyAlerts { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
} 