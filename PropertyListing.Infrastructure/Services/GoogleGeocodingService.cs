using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PropertyListing.Application.Common.Interfaces;

namespace PropertyListing.Infrastructure.Services;

public class GoogleGeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GoogleGeocodingService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["GoogleMaps:ApiKey"] 
            ?? throw new ArgumentNullException("GoogleMaps:ApiKey configuration is missing");
    }

    public async Task<(double Latitude, double Longitude, string FormattedAddress)> GetCoordinatesAsync(
        string address, string? city = null, string? district = null)
    {
        var searchAddress = $"{address}, {district}, {city}".Trim(' ', ',');
        var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(searchAddress)}&key={_apiKey}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<GoogleGeocodingResponse>(content);

        if (result?.Results == null || !result.Results.Any())
            throw new Exception("No results found");

        var location = result.Results[0].Geometry.Location;
        return (location.Lat, location.Lng, result.Results[0].FormattedAddress);
    }

    private class GoogleGeocodingResponse
    {
        public List<GeocodingResult> Results { get; set; } = new();
    }

    private class GeocodingResult
    {
        public string FormattedAddress { get; set; } = string.Empty;
        public Geometry Geometry { get; set; } = new();
    }

    private class Geometry
    {
        public Location Location { get; set; } = new();
    }

    private class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
} 