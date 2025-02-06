public interface IAddressService
{
    Task<IEnumerable<string>> GetCitiesAsync();
    Task<IEnumerable<string>> GetDistrictsAsync(string city);
} 