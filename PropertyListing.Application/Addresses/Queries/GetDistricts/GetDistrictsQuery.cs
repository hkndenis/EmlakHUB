using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Addresses.Queries.GetDistricts;

public record GetDistrictsQuery(string City, string? Search = null) : IRequest<Result<List<string>>>;

public class GetDistrictsQueryHandler : IRequestHandler<GetDistrictsQuery, Result<List<string>>>
{
    private static readonly Dictionary<string, List<string>> Districts = new()
    {
        ["İstanbul"] = new() {
            "Adalar", "Arnavutköy", "Ataşehir", "Avcılar", "Bağcılar", "Bahçelievler", "Bakırköy", "Başakşehir",
            "Bayrampaşa", "Beşiktaş", "Beykoz", "Beylikdüzü", "Beyoğlu", "Büyükçekmece", "Çatalca", "Çekmeköy",
            "Esenler", "Esenyurt", "Eyüpsultan", "Fatih", "Gaziosmanpaşa", "Güngören", "Kadıköy", "Kağıthane",
            "Kartal", "Küçükçekmece", "Maltepe", "Pendik", "Sancaktepe", "Sarıyer", "Silivri", "Sultanbeyli",
            "Sultangazi", "Şile", "Şişli", "Tuzla", "Ümraniye", "Üsküdar", "Zeytinburnu"
        },
        ["Ankara"] = new() {
            "Akyurt", "Altındağ", "Ayaş", "Balâ", "Beypazarı", "Çamlıdere", "Çankaya", "Çubuk", "Elmadağ",
            "Etimesgut", "Evren", "Gölbaşı", "Güdül", "Haymana", "Kalecik", "Kahramankazan", "Keçiören",
            "Kızılcahamam", "Mamak", "Nallıhan", "Polatlı", "Pursaklar", "Sincan", "Şereflikoçhisar", "Yenimahalle"
        },
        ["İzmir"] = new() {
            "Aliağa", "Balçova", "Bayındır", "Bayraklı", "Bergama", "Beydağ", "Bornova", "Buca", "Çeşme",
            "Çiğli", "Dikili", "Foça", "Gaziemir", "Güzelbahçe", "Karabağlar", "Karaburun", "Karşıyaka",
            "Kemalpaşa", "Kınık", "Kiraz", "Konak", "Menderes", "Menemen", "Narlıdere", "Ödemiş", "Seferihisar",
            "Selçuk", "Tire", "Torbalı", "Urla"
        },
        ["Bursa"] = new() {
            "Büyükorhan", "Gemlik", "Gürsu", "Harmancık", "İnegöl", "İznik", "Karacabey", "Keles", "Kestel",
            "Mudanya", "Mustafakemalpaşa", "Nilüfer", "Orhaneli", "Orhangazi", "Osmangazi", "Yenişehir", "Yıldırım"
        },
        ["Antalya"] = new() {
            "Akseki", "Aksu", "Alanya", "Demre", "Döşemealtı", "Elmalı", "Finike", "Gazipaşa", "Gündoğmuş",
            "İbradı", "Kaş", "Kemer", "Kepez", "Konyaaltı", "Korkuteli", "Kumluca", "Manavgat", "Muratpaşa",
            "Serik"
        },
        ["Adana"] = new() {
            "Aladağ", "Ceyhan", "Çukurova", "Feke", "İmamoğlu", "Karaisalı", "Karataş", "Kozan", "Pozantı",
            "Saimbeyli", "Sarıçam", "Seyhan", "Tufanbeyli", "Yumurtalık", "Yüreğir"
        },
        ["Konya"] = new() {
            "Ahırlı", "Akören", "Akşehir", "Altınekin", "Beyşehir", "Bozkır", "Çeltik", "Cihanbeyli", "Çumra",
            "Derbent", "Derebucak", "Doğanhisar", "Emirgazi", "Ereğli", "Güneysınır", "Hadim", "Halkapınar",
            "Hüyük", "Ilgın", "Kadınhanı", "Karapınar", "Karatay", "Kulu", "Meram", "Sarayönü", "Selçuklu",
            "Seydişehir", "Taşkent", "Tuzlukçu", "Yalıhüyük", "Yunak"
        },
        ["Gaziantep"] = new() {
            "Araban", "İslahiye", "Karkamış", "Nizip", "Nurdağı", "Oğuzeli", "Şahinbey", "Şehitkamil", "Yavuzeli"
        }
    };

    public Task<Result<List<string>>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
    {
        if (!Districts.ContainsKey(request.City))
            return Task.FromResult(Result<List<string>>.Success(new List<string>()));

        var query = Districts[request.City].AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(d => d.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(Result<List<string>>.Success(query.ToList()));
    }
} 