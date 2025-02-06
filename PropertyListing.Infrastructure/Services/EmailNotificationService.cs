using Microsoft.Extensions.Logging;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Properties.Queries.Dtos;

namespace PropertyListing.Infrastructure.Services;

public class EmailNotificationService : INotificationService
{
    private readonly ILogger<EmailNotificationService> _logger;
    // Email servisini buraya ekleyebilirsiniz (örn: SendGrid, SMTP vs.)

    public EmailNotificationService(ILogger<EmailNotificationService> logger)
    {
        _logger = logger;
    }

    public async Task SendPropertyAlertAsync(
        string userEmail,
        string userName,
        List<PropertyDto> matchingProperties,
        CancellationToken cancellationToken = default)
    {
        // Şimdilik sadece log atalım
        _logger.LogInformation(
            "Alert email would be sent to {UserEmail} about {Count} properties",
            userEmail,
            matchingProperties.Count);

        // TODO: Email gönderme mantığı eklenecek
        await Task.CompletedTask;
    }
} 