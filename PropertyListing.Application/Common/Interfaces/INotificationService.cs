using PropertyListing.Application.Properties.Queries.Dtos;

namespace PropertyListing.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendPropertyAlertAsync(
        string userEmail,
        string userName,
        List<PropertyDto> matchingProperties,
        CancellationToken cancellationToken = default);
} 