using Microsoft.AspNetCore.Mvc;
using PropertyListing.Application.Addresses.Queries.GetCoordinates;
using PropertyListing.Application.Addresses.Queries.GetCities;
using PropertyListing.Application.Addresses.Queries.GetDistricts;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace PropertyListing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : ApiControllerBase
{
    private static readonly Dictionary<string, List<string>> CityDistricts = new()
    {
        ["İstanbul"] = new List<string> { 
            "Adalar", "Arnavutköy", "Ataşehir", "Avcılar", "Bağcılar", "Bahçelievler", "Bakırköy", "Başakşehir",
            "Bayrampaşa", "Beşiktaş", "Beykoz", "Beylikdüzü", "Beyoğlu", "Büyükçekmece", "Çatalca", "Çekmeköy",
            "Esenler", "Esenyurt", "Eyüpsultan", "Fatih", "Gaziosmanpaşa", "Güngören", "Kadıköy", "Kağıthane",
            "Kartal", "Küçükçekmece", "Maltepe", "Pendik", "Sancaktepe", "Sarıyer", "Silivri", "Sultanbeyli",
            "Sultangazi", "Şile", "Şişli", "Tuzla", "Ümraniye", "Üsküdar", "Zeytinburnu"
        },
        ["Ankara"] = new List<string> { 
            "Akyurt", "Altındağ", "Ayaş", "Bala", "Beypazarı", "Çamlıdere", "Çankaya", "Çubuk", "Elmadağ",
            "Etimesgut", "Evren", "Gölbaşı", "Güdül", "Haymana", "Kalecik", "Kahramankazan", "Keçiören",
            "Kızılcahamam", "Mamak", "Nallıhan", "Polatlı", "Pursaklar", "Sincan", "Şereflikoçhisar", "Yenimahalle"
        },
        ["İzmir"] = new List<string> { 
            "Aliağa", "Balçova", "Bayındır", "Bayraklı", "Bergama", "Beydağ", "Bornova", "Buca", "Çeşme",
            "Çiğli", "Dikili", "Foça", "Gaziemir", "Güzelbahçe", "Karabağlar", "Karaburun", "Karşıyaka",
            "Kemalpaşa", "Kınık", "Kiraz", "Konak", "Menderes", "Menemen", "Narlıdere", "Ödemiş", "Seferihisar",
            "Selçuk", "Tire", "Torbalı", "Urla"
        },
        ["Bursa"] = new List<string> { 
            "Büyükorhan", "Gemlik", "Gürsu", "Harmancık", "İnegöl", "İznik", "Karacabey", "Keles", "Kestel",
            "Mudanya", "Mustafakemalpaşa", "Nilüfer", "Orhaneli", "Orhangazi", "Osmangazi", "Yenişehir", "Yıldırım"
        },
        ["Antalya"] = new List<string> { 
            "Akseki", "Aksu", "Alanya", "Demre", "Döşemealtı", "Elmalı", "Finike", "Gazipaşa", "Gündoğmuş",
            "İbradı", "Kaş", "Kemer", "Kepez", "Konyaaltı", "Korkuteli", "Kumluca", "Manavgat", "Muratpaşa",
            "Serik"
        },
        // Diğer iller (alfabetik sırayla)
        ["Adana"] = new List<string>(),
        ["Adıyaman"] = new List<string>(),
        ["Afyonkarahisar"] = new List<string>(),
        ["Ağrı"] = new List<string>(),
        ["Aksaray"] = new List<string>(),
        ["Amasya"] = new List<string>(),
        ["Ardahan"] = new List<string>(),
        ["Artvin"] = new List<string>(),
        ["Aydın"] = new List<string>(),
        ["Balıkesir"] = new List<string>(),
        ["Bartın"] = new List<string>(),
        ["Batman"] = new List<string>(),
        ["Bayburt"] = new List<string>(),
        ["Bilecik"] = new List<string>(),
        ["Bingöl"] = new List<string>(),
        ["Bitlis"] = new List<string>(),
        ["Bolu"] = new List<string>(),
        ["Burdur"] = new List<string>(),
        ["Çanakkale"] = new List<string>(),
        ["Çankırı"] = new List<string>(),
        ["Çorum"] = new List<string>(),
        ["Denizli"] = new List<string>(),
        ["Diyarbakır"] = new List<string>(),
        ["Düzce"] = new List<string>(),
        ["Edirne"] = new List<string>(),
        ["Elazığ"] = new List<string>(),
        ["Erzincan"] = new List<string>(),
        ["Erzurum"] = new List<string>(),
        ["Eskişehir"] = new List<string>(),
        ["Gaziantep"] = new List<string>(),
        ["Giresun"] = new List<string>(),
        ["Gümüşhane"] = new List<string>(),
        ["Hakkari"] = new List<string>(),
        ["Hatay"] = new List<string>(),
        ["Iğdır"] = new List<string>(),
        ["Isparta"] = new List<string>(),
        ["Kahramanmaraş"] = new List<string>(),
        ["Karabük"] = new List<string>(),
        ["Karaman"] = new List<string>(),
        ["Kars"] = new List<string>(),
        ["Kastamonu"] = new List<string>(),
        ["Kayseri"] = new List<string>(),
        ["Kırıkkale"] = new List<string>(),
        ["Kırklareli"] = new List<string>(),
        ["Kırşehir"] = new List<string>(),
        ["Kilis"] = new List<string>(),
        ["Kocaeli"] = new List<string>(),
        ["Konya"] = new List<string>(),
        ["Kütahya"] = new List<string>(),
        ["Malatya"] = new List<string>(),
        ["Manisa"] = new List<string>(),
        ["Mardin"] = new List<string>(),
        ["Mersin"] = new List<string>(),
        ["Muğla"] = new List<string>(),
        ["Muş"] = new List<string>(),
        ["Nevşehir"] = new List<string>(),
        ["Niğde"] = new List<string>(),
        ["Ordu"] = new List<string>(),
        ["Osmaniye"] = new List<string>(),
        ["Rize"] = new List<string>(),
        ["Sakarya"] = new List<string>(),
        ["Samsun"] = new List<string>(),
        ["Siirt"] = new List<string>(),
        ["Sinop"] = new List<string>(),
        ["Sivas"] = new List<string>(),
        ["Şanlıurfa"] = new List<string>(),
        ["Şırnak"] = new List<string>(),
        ["Tekirdağ"] = new List<string>(),
        ["Tokat"] = new List<string>(),
        ["Trabzon"] = new List<string>(),
        ["Tunceli"] = new List<string>(),
        ["Uşak"] = new List<string>(),
        ["Van"] = new List<string>(),
        ["Yalova"] = new List<string>(),
        ["Yozgat"] = new List<string>(),
        ["Zonguldak"] = new List<string>()
    };

    private readonly ILogger<AddressesController> _logger;

    public AddressesController(ILogger<AddressesController> logger)
    {
        _logger = logger;
    }

    [HttpGet("coordinates")]
    [SwaggerOperation(
        Summary = "Adres bilgisinden koordinat al",
        Description = "Verilen adres bilgisini kullanarak enlem ve boylam koordinatlarını döner")]
    public async Task<IActionResult> GetCoordinates([FromQuery] GetCoordinatesQuery query)
    {
        var result = await Mediator.Send(query);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }

    [HttpGet("cities")]
    [SwaggerOperation(
        Summary = "Şehirleri listele",
        Description = "Türkiye'deki büyük şehirleri listeler")]
    public ActionResult<IEnumerable<string>> GetCities()
    {
        _logger.LogInformation("Getting cities...");
        var cities = CityDistricts.Keys.OrderBy(c => c).ToList();
        _logger.LogInformation($"Found {cities.Count} cities");
        return Ok(cities);
    }

    [HttpGet("districts")]
    [SwaggerOperation(
        Summary = "İlçeleri listele",
        Description = "Seçilen şehre göre ilçeleri listeler")]
    public ActionResult<IEnumerable<string>> GetDistricts([FromQuery] string city)
    {
        if (string.IsNullOrEmpty(city))
            return BadRequest("Şehir parametresi gereklidir.");

        if (!CityDistricts.ContainsKey(city))
            return NotFound($"'{city}' için ilçe bilgisi bulunamadı.");

        var districts = CityDistricts[city].OrderBy(d => d).ToList();
        return Ok(districts);
    }
} 