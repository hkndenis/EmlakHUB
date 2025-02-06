using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Properties.Queries.Dtos;
using PropertyListing.Domain.Enums;
using PropertyListing.Domain.ValueObjects;  // Money için

namespace PropertyListing.Application.Properties.Queries.GetProperties;

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, Result<List<PropertyDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPropertiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<PropertyDto>>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        // Fiyat aralıklarını düzelt
        if (request.MinSalePrice.HasValue && request.MaxSalePrice.HasValue 
            && request.MinSalePrice.Value > request.MaxSalePrice.Value)
        {
            var temp = request.MinSalePrice;
            request = request with { MinSalePrice = request.MaxSalePrice, MaxSalePrice = temp };
        }

        if (request.MinRentPrice.HasValue && request.MaxRentPrice.HasValue 
            && request.MinRentPrice.Value > request.MaxRentPrice.Value)
        {
            var temp = request.MinRentPrice;
            request = request with { MinRentPrice = request.MaxRentPrice, MaxRentPrice = temp };
        }

        var query = _context.Properties
            .Include(p => p.Features)
            .Include(p => p.Images)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(p => 
                p.Title.Contains(request.SearchTerm) || 
                p.Description.Contains(request.SearchTerm));
        }

        if (!string.IsNullOrWhiteSpace(request.City))
        {
            query = query.Where(p => p.Location.City == request.City);
        }

        if (request.Type.HasValue)
        {
            query = query.Where(p => p.Type == request.Type.Value);
        }

        // Genel fiyat filtresi
        if (request.MinPrice.HasValue)
        {
            var minPrice = Money.FromDecimal(request.MinPrice.Value);
            query = query.Where(p => p.Price >= minPrice);
        }

        if (request.MaxPrice.HasValue)
        {
            var maxPrice = Money.FromDecimal(request.MaxPrice.Value);
            query = query.Where(p => p.Price <= maxPrice);
        }

        // Type'a özel fiyat filtreleri
        if (request.MinSalePrice.HasValue)
        {
            query = query.Where(p => 
                p.Type == PropertyType.ForSale && 
                EF.Property<decimal>(p.Price, "Amount") >= request.MinSalePrice.Value);
        }

        if (request.MaxSalePrice.HasValue)
        {
            query = query.Where(p => 
                p.Type == PropertyType.ForSale && 
                EF.Property<decimal>(p.Price, "Amount") <= request.MaxSalePrice.Value);
        }

        if (request.MinRentPrice.HasValue)
        {
            query = query.Where(p => 
                p.Type == PropertyType.ForRent && 
                EF.Property<decimal>(p.Price, "Amount") >= request.MinRentPrice.Value);
        }

        if (request.MaxRentPrice.HasValue)
        {
            query = query.Where(p => 
                p.Type == PropertyType.ForRent && 
                EF.Property<decimal>(p.Price, "Amount") <= request.MaxRentPrice.Value);
        }

        if (request.MinBedrooms.HasValue)
        {
            query = query.Where(p => p.Bedrooms >= request.MinBedrooms.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(p => p.Status == request.Status.Value);
        }

        if (request.Statuses != null && request.Statuses.Any())
        {
            query = query.Where(p => request.Statuses.Contains(p.Status));
        }

        if (request.OrderByViewCount)
        {
            query = query.OrderByDescending(p => p.ViewCount);
        }

        var properties = await query.ToListAsync(cancellationToken);
        var propertyDtos = _mapper.Map<List<PropertyDto>>(properties);

        // Mesafe hesaplama ve filtreleme
        if (request.Latitude.HasValue && request.Longitude.HasValue)
        {
            var filteredProperties = propertyDtos.Select(p =>
            {
                p.DistanceInKm = CalculateDistance(
                    request.Latitude.Value,
                    request.Longitude.Value,
                    p.Location.Latitude,
                    p.Location.Longitude);
                return p;
            });

            if (request.RadiusInKm.HasValue)
            {
                filteredProperties = filteredProperties.Where(p => 
                    p.DistanceInKm <= request.RadiusInKm.Value);
            }

            if (request.OrderByDistance)
            {
                filteredProperties = filteredProperties.OrderBy(p => p.DistanceInKm);
            }

            return Result<List<PropertyDto>>.Success(filteredProperties.ToList());
        }

        return Result<List<PropertyDto>>.Success(propertyDtos);
    }

    // Haversine formülü ile iki nokta arasındaki mesafeyi hesapla (km cinsinden)
    private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371; // Dünya'nın yarıçapı (km)
        var dLat = ToRad(lat2 - lat1);
        var dLon = ToRad(lon2 - lon1);
        
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private static double ToRad(double degree)
    {
        return degree * Math.PI / 180;
    }
} 