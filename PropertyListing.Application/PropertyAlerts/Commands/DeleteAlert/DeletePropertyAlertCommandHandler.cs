using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.PropertyAlerts.Commands.DeleteAlert;

public class DeletePropertyAlertCommandHandler : IRequestHandler<DeletePropertyAlertCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeletePropertyAlertCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Unit>> Handle(DeletePropertyAlertCommand request, CancellationToken cancellationToken)
    {
        var alert = await _context.PropertyAlerts
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUserService.UserId, cancellationToken);

        if (alert == null)
            return Result<Unit>.Failure("Alert not found");

        _context.PropertyAlerts.Remove(alert);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
} 