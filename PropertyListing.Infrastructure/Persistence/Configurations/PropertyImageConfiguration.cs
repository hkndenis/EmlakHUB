using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Infrastructure.Persistence.Configurations;

public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
{
    public void Configure(EntityTypeBuilder<PropertyImage> builder)
    {
        builder.Property(p => p.Id)
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Url)
            .HasMaxLength(500)
            .IsRequired();

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

        builder.HasOne(p => p.Property)
            .WithMany(p => p.Images)
            .HasForeignKey(p => p.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 