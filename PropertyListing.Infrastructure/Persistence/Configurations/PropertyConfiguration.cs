using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Infrastructure.Persistence.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Properties");

        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(p => p.Type)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasMaxLength(20)
            .IsRequired();

        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(m => m.Amount)
                .HasColumnName("Price")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            priceBuilder.Property(m => m.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.OwnsOne(p => p.Location, locationBuilder =>
        {
            locationBuilder.Property(l => l.City)
                .HasColumnName("City")
                .HasMaxLength(100)
                .IsRequired();

            locationBuilder.Property(l => l.District)
                .HasColumnName("District")
                .HasMaxLength(100)
                .IsRequired();

            locationBuilder.Property(l => l.PostalCode)
                .HasColumnName("PostalCode")
                .HasMaxLength(10);

            locationBuilder.Property(l => l.Latitude)
                .HasColumnName("Latitude")
                .HasColumnType("double precision")
                .IsRequired();

            locationBuilder.Property(l => l.Longitude)
                .HasColumnName("Longitude")
                .HasColumnType("double precision")
                .IsRequired();
        });

        builder.Property(p => p.CreatedBy)
            .HasMaxLength(100)
            .HasDefaultValue("system");

        builder.Property(p => p.LastModifiedBy)
            .HasMaxLength(100)
            .HasDefaultValue("system");

        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp with time zone");

        builder.Property(p => p.LastModifiedAt)
            .HasColumnType("timestamp with time zone");

        builder.HasMany(p => p.Images)
            .WithOne()
            .HasForeignKey(i => i.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Features)
            .WithOne()
            .HasForeignKey(f => f.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.StatusHistory)
            .WithOne(h => h.Property)
            .HasForeignKey(h => h.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 