using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Properties.Queries.Dtos;
using PropertyListing.Application.Properties.Features.Dtos;
using PropertyListing.Domain.Entities;
using PropertyListing.Domain.ValueObjects;

namespace PropertyListing.Application.Properties.Queries.CompareProperties;

public class ComparePropertiesQueryHandler : IRequestHandler<ComparePropertiesQuery, Result<ComparisonDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ComparePropertiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<ComparisonDto>> Handle(ComparePropertiesQuery request, CancellationToken cancellationToken)
    {
        var properties = await _context.Properties
            .Include(p => p.Location)
            .Include(p => p.Features)
            .Include(p => p.Images)
            .Where(p => request.PropertyIds.Contains(p.Id))
            .Select(p => _mapper.Map<PropertyComparisonDto>(p))
            .ToListAsync(cancellationToken);

        if (properties.Count < 2)
            return Result<ComparisonDto>.Failure("At least 2 properties are required for comparison.");

        var comparison = new ComparisonDto
        {
            Properties = properties,
            Differences = FindDifferences(properties),
            Similarities = FindSimilarities(properties)
        };

        return Result<ComparisonDto>.Success(comparison);
    }

    private Dictionary<string, List<string>> FindDifferences(List<PropertyComparisonDto> properties)
    {
        var differences = new Dictionary<string, List<string>>();

        // Fiyat farklılıkları
        differences["Price"] = properties.Select(p => $"{p.Price} {p.Currency}").Distinct().ToList();

        // Oda sayısı farklılıkları
        differences["Bedrooms"] = properties.Select(p => p.Bedrooms.ToString()).Distinct().ToList();

        // Metrekare farklılıkları
        differences["SquareMeters"] = properties.Select(p => p.SquareMeters.ToString()).Distinct().ToList();

        // Özellik farklılıkları
        differences["Features"] = properties
            .SelectMany(p => p.Features.Select(f => f.Name))
            .Distinct()
            .ToList();

        return differences;
    }

    private Dictionary<string, List<string>> FindSimilarities(List<PropertyComparisonDto> properties)
    {
        var similarities = new Dictionary<string, List<string>>();

        // Aynı şehir/ilçe
        if (properties.Select(p => p.Location.City).Distinct().Count() == 1)
        {
            similarities["City"] = new List<string> { properties.First().Location.City };
        }

        // Aynı tip (kiralık/satılık)
        if (properties.Select(p => p.Type).Distinct().Count() == 1)
        {
            similarities["Type"] = new List<string> { properties.First().Type.ToString() };
        }

        // Ortak özellikler
        var commonFeatures = properties
            .SelectMany(p => p.Features.Select(f => f.Name))
            .GroupBy(f => f)
            .Where(g => g.Count() == properties.Count)
            .Select(g => g.Key)
            .ToList();

        if (commonFeatures.Any())
        {
            similarities["Features"] = commonFeatures;
        }

        return similarities;
    }
} 