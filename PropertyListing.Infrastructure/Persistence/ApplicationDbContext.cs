using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Domain.Entities;
using PropertyListing.Infrastructure.Persistence.Extensions;
using System.Reflection;

namespace PropertyListing.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Sadece NoTracking'i kullanalım
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Property> Properties => Set<Property>();
        public DbSet<PropertyImage> PropertyImages => Set<PropertyImage>();
        public DbSet<PropertyFeature> PropertyFeatures => Set<PropertyFeature>();
        public DbSet<PropertyStatusHistory> PropertyStatusHistory { get; set; }
        public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
        public DbSet<Favorite> Favorites => Set<Favorite>();
        public DbSet<PropertyAlert> PropertyAlerts => Set<PropertyAlert>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyAllConfigurations();
            base.OnModelCreating(builder);
        }
    }
} 