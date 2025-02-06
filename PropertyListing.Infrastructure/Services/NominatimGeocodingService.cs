using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PropertyListing.Application.Common.Interfaces;

namespace PropertyListing.Infrastructure.Services
{
    public class NominatimGeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;

        public NominatimGeocodingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PropertyListing/1.0");
        }

        public async Task<(double Latitude, double Longitude, string FormattedAddress)> GetCoordinatesAsync(
            string address, string? city = null, string? district = null)
        {
            try 
            {
                var searchAddress = $"{address}, {district}, {city}, Türkiye"
                    .Trim(' ', ',')
                    .Replace("İ", "I")
                    .Replace("ı", "i")
                    .Replace("Ş", "S")
                    .Replace("ş", "s")
                    .Replace("Ğ", "G")
                    .Replace("ğ", "g")
                    .Replace("Ü", "U")
                    .Replace("ü", "u")
                    .Replace("Ö", "O")
                    .Replace("ö", "o")
                    .Replace("Ç", "C")
                    .Replace("ç", "c");

                var url = $"search?format=json&q={Uri.EscapeDataString(searchAddress)}&countrycodes=tr&limit=1";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Nominatim Response: {content}"); // Debug için

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var results = JsonSerializer.Deserialize<List<NominatimResult>>(content, options);

                if (results == null || !results.Any())
                    throw new Exception($"No results found for address: {searchAddress}");

                var result = results.First();
                Console.WriteLine($"Lat: {result.Lat}, Lon: {result.Lon}"); // Debug için

                // NumberStyles.Float kullan
                if (!double.TryParse(result.Lat, NumberStyles.Float, CultureInfo.InvariantCulture, out var latitude) ||
                    !double.TryParse(result.Lon, NumberStyles.Float, CultureInfo.InvariantCulture, out var longitude))
                {
                    throw new Exception($"Invalid coordinates returned from service. Lat: {result.Lat}, Lon: {result.Lon}");
                }

                return (latitude, longitude, result.DisplayName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Geocoding failed: {ex.Message}");
            }
        }

        private class NominatimResult
        {
            public string Lat { get; set; } = string.Empty;
            public string Lon { get; set; } = string.Empty;
            [JsonPropertyName("display_name")]
            public string DisplayName { get; set; } = string.Empty;
        }
    }
} 