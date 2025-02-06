using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyListing.Application.Common.Interfaces;

namespace PropertyListing.Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private static readonly Dictionary<string, List<string>> CityDistricts = new()
        {
            ["İstanbul"] = new List<string> { "Kadıköy", "Beşiktaş", "Üsküdar", "Şişli", "Maltepe", "Ataşehir" },
            ["Ankara"] = new List<string> { "Çankaya", "Keçiören", "Yenimahalle", "Mamak", "Etimesgut" },
            ["İzmir"] = new List<string> { "Konak", "Karşıyaka", "Bornova", "Buca", "Çiğli" },
            ["Bursa"] = new List<string> { "Nilüfer", "Osmangazi", "Yıldırım", "Mudanya" },
            ["Antalya"] = new List<string> { "Muratpaşa", "Konyaaltı", "Kepez", "Döşemealtı" }
        };

        public Task<IEnumerable<string>> GetCitiesAsync()
        {
            return Task.FromResult<IEnumerable<string>>(CityDistricts.Keys);
        }

        public Task<IEnumerable<string>> GetDistrictsAsync(string city)
        {
            if (!CityDistricts.ContainsKey(city))
                return Task.FromResult<IEnumerable<string>>(Array.Empty<string>());

            return Task.FromResult<IEnumerable<string>>(CityDistricts[city]);
        }
    }
} 