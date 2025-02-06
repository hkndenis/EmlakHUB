using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.PropertyAlerts.Commands.ToggleAlert;

public class TogglePropertyAlertCommandHandler : IRequestHandler<TogglePropertyAlertCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public TogglePropertyAlertCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Unit>> Handle(TogglePropertyAlertCommand request, CancellationToken cancellationToken)
    {
        var alert = await _context.PropertyAlerts
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUserService.UserId, cancellationToken);

        if (alert == null)
            return Result<Unit>.Failure("Alert not found");

        alert.IsActive = !alert.IsActive;
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
} 