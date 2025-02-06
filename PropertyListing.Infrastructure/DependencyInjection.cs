using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Infrastructure.Persistence;
using PropertyListing.Infrastructure.Services;

namespace PropertyListing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => 
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    b.EnableRetryOnFailure();
                });
        });

        services.AddScoped<IApplicationDbContext>(provider => 
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IIdentityService, IdentityService>();

        // Google Maps yerine Nominatim kullan
        services.AddHttpClient<IGeocodingService, NominatimGeocodingService>();

        services.AddScoped<INotificationService, EmailNotificationService>();
        services.AddHostedService<PropertyAlertBackgroundService>();

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
} 