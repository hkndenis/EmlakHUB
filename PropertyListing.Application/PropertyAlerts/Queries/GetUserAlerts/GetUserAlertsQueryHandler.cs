using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.PropertyAlerts.Queries.GetUserAlerts;

public class GetUserAlertsQueryHandler : IRequestHandler<GetUserAlertsQuery, Result<List<PropertyAlertDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetUserAlertsQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<Result<List<PropertyAlertDto>>> Handle(GetUserAlertsQuery request, CancellationToken cancellationToken)
    {
        var alerts = await _context.PropertyAlerts
            .Where(a => a.UserId == _currentUserService.UserId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return Result<List<PropertyAlertDto>>.Success(
            _mapper.Map<List<PropertyAlertDto>>(alerts));
    }
} 