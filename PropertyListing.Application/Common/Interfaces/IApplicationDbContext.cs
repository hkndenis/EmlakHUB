using Microsoft.EntityFrameworkCore;
using PropertyListing.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PropertyListing.Application.Common.Interfaces
{
    /// <summary>
    /// Veritabanı bağlamı için soyutlama katmanı
    /// Abstraction layer for database context
    /// </summary>
    public interface IApplicationDbContext
    {
        /// <summary>
        /// Emlak ilanları tablosu
        /// Property listings table
        /// </summary>
        DbSet<Property> Properties { get; }
        
        /// <summary>
        /// Emlak özellikleri tablosu
        /// Property features table
        /// </summary>
        DbSet<PropertyFeature> PropertyFeatures { get; }
        
        /// <summary>
        /// Emlak görselleri tablosu
        /// Property images table
        /// </summary>
        DbSet<PropertyImage> PropertyImages { get; }
        
        /// <summary>
        /// Uygulama kullanıcıları tablosu
        /// Application users table
        /// </summary>
        DbSet<ApplicationUser> Users { get; }
        
        /// <summary>
        /// Favori ilanlar tablosu
        /// Favorite listings table
        /// </summary>
        DbSet<Favorite> Favorites { get; }
        
        /// <summary>
        /// İlan durum geçmişi tablosu
        /// Property status history table
        /// </summary>
        DbSet<PropertyStatusHistory> PropertyStatusHistory { get; }
        
        /// <summary>
        /// İlan bildirimleri tablosu
        /// Property alerts table
        /// </summary>
        DbSet<PropertyAlert> PropertyAlerts { get; }
        
        /// <summary>
        /// Veritabanı değişikliklerini kaydeder
        /// Saves database changes
        /// </summary>
        /// <param name="cancellationToken">İptal token'ı / Cancellation token</param>
        /// <returns>Etkilenen kayıt sayısı / Number of affected records</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
} 