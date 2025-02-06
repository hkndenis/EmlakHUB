using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Properties.Queries.Dtos;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Infrastructure.Services;

public class PropertyAlertBackgroundService : BackgroundService
{
    private readonly ILogger<PropertyAlertBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(15); // Her 15 dakikada bir kontrol et

    public PropertyAlertBackgroundService(
        ILogger<PropertyAlertBackgroundService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckPropertyAlerts(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking property alerts");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task CheckPropertyAlerts(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

        var alerts = await context.PropertyAlerts
            .Include(a => a.User)
            .Where(a => a.IsActive)
            .ToListAsync(cancellationToken);

        foreach (var alert in alerts)
        {
            var query = context.Properties.AsQueryable();

            // Filtreleri uygula
            if (!string.IsNullOrEmpty(alert.City))
                query = query.Where(p => p.Location.City == alert.City);

            if (!string.IsNullOrEmpty(alert.District))
                query = query.Where(p => p.Location.District == alert.District);

            if (alert.MaxPrice.HasValue)
            {
                var price = EF.Property<decimal>(alert.MaxPrice, "Amount");
                query = query.Where(p => EF.Property<decimal>(p.Price, "Amount") <= price);
            }

            if (alert.MinPrice.HasValue)
            {
                var price = EF.Property<decimal>(alert.MinPrice, "Amount");
                query = query.Where(p => EF.Property<decimal>(p.Price, "Amount") >= price);
            }

            if (alert.MinBedrooms.HasValue)
                query = query.Where(p => p.Bedrooms >= alert.MinBedrooms.Value);

            if (alert.MinSquareMeters.HasValue)
                query = query.Where(p => p.SquareMeters >= alert.MinSquareMeters.Value);

            query = query.Where(p => p.Type == alert.Type);
            query = query.Where(p => p.CreatedAt > alert.LastNotificationSent);

            var matchingProperties = await query
                .Select(p => new PropertyDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = EF.Property<decimal>(p.Price, "Amount"),
                    Currency = EF.Property<string>(p.Price, "Currency"),
                    // ... diğer özellikler
                })
                .ToListAsync(cancellationToken);

            if (matchingProperties.Any())
            {
                await notificationService.SendPropertyAlertAsync(
                    alert.User.Email,
                    alert.User.FirstName,
                    matchingProperties,
                    cancellationToken);

                alert.LastNotificationSent = DateTime.UtcNow;
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
} 