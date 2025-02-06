using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Infrastructure.Persistence.Configurations;

public class PropertyStatusHistoryConfiguration : IEntityTypeConfiguration<PropertyStatusHistory>
{
    public void Configure(EntityTypeBuilder<PropertyStatusHistory> builder)
    {
        builder.Property(p => p.Id)
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Note)
            .HasMaxLength(500);

        builder.Property(p => p.CreatedBy)
            .HasMaxLength(100)
            .HasDefaultValue("system");

        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp with time zone");

        builder.HasOne(p => p.Property)
            .WithMany(p => p.StatusHistory)
            .HasForeignKey(p => p.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 