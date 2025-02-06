using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.Id)
            .HasColumnType("varchar(450)");

        builder.Property(p => p.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.Property(p => p.PasswordHash)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(p => p.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(20);

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

        builder.Property(p => p.LastLoginDate)
            .HasColumnType("timestamp with time zone");
    }
} 