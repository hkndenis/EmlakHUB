using System.Threading.Tasks;

namespace PropertyListing.Application.Common.Interfaces;

public interface IGeocodingService
{
    Task<(double Latitude, double Longitude, string FormattedAddress)> GetCoordinatesAsync(
        string address, 
        string? city = null, 
        string? district = null);
} 