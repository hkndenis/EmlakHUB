using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Queries.GetStatusHistory;

public class GetPropertyStatusHistoryQueryHandler 
    : IRequestHandler<GetPropertyStatusHistoryQuery, Result<List<StatusHistoryDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPropertyStatusHistoryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<StatusHistoryDto>>> Handle(
        GetPropertyStatusHistoryQuery request, 
        CancellationToken cancellationToken)
    {
        var history = await _context.PropertyStatusHistory
            .Where(h => h.PropertyId == request.PropertyId)
            .OrderByDescending(h => h.CreatedAt)
            .ToListAsync(cancellationToken);

        return Result<List<StatusHistoryDto>>.Success(
            _mapper.Map<List<StatusHistoryDto>>(history));
    }
} 