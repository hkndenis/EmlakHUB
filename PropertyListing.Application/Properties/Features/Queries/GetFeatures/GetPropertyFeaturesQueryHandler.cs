using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Properties.Features.Dtos;

namespace PropertyListing.Application.Properties.Features.Queries.GetFeatures;

public class GetPropertyFeaturesQueryHandler : IRequestHandler<GetPropertyFeaturesQuery, Result<List<PropertyFeatureDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPropertyFeaturesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<PropertyFeatureDto>>> Handle(GetPropertyFeaturesQuery request, CancellationToken cancellationToken)
    {
        var features = await _context.PropertyFeatures
            .Where(f => f.PropertyId == request.PropertyId)
            .ToListAsync(cancellationToken);

        return Result<List<PropertyFeatureDto>>.Success(
            _mapper.Map<List<PropertyFeatureDto>>(features));
    }
} 