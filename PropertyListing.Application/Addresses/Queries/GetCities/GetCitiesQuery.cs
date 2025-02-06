using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Addresses.Queries.GetCities;

public record GetCitiesQuery(string? Search = null) : IRequest<Result<List<string>>>;

public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, Result<List<string>>>
{
    private static readonly List<string> Cities = new()
    {
        "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin", "Aydın", "Balıkesir",
        "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı", "Çorum", "Denizli",
        "Diyarbakır", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari",
        "Hatay", "Isparta", "Mersin", "İstanbul", "İzmir", "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir",
        "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir",
        "Niğde", "Ordu", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat",
        "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van", "Yozgat", "Zonguldak", "Aksaray", "Bayburt", "Karaman",
        "Kırıkkale", "Batman", "Şırnak", "Bartın", "Ardahan", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce"
    };

    public Task<Result<List<string>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var query = Cities.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(c => c.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(Result<List<string>>.Success(query.ToList()));
    }
} 