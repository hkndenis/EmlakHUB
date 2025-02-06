using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Infrastructure.Persistence.Configurations;

public class PropertyAlertConfiguration : IEntityTypeConfiguration<PropertyAlert>
{
    public void Configure(EntityTypeBuilder<PropertyAlert> builder)
    {
        builder.Property(e => e.City)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.District)
            .HasMaxLength(100);

        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 