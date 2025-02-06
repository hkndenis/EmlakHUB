using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Properties.Queries.Dtos;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Application.Properties.Queries.GetPropertyById;

public record GetPropertyByIdQuery : IRequest<Result<PropertyDto>>
{
    public Guid Id { get; init; }
}

public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, Result<PropertyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPropertyByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PropertyDto>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .Include(p => p.Location)
            .Include(p => p.Features)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (property == null)
            return Result<PropertyDto>.Failure("Property not found.");

        var propertyDto = _mapper.Map<PropertyDto>(property);
        return Result<PropertyDto>.Success(propertyDto);
    }
} 