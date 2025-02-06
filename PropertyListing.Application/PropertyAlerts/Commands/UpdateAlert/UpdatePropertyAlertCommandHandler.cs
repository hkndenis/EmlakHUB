using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.PropertyAlerts.Commands.UpdateAlert;

public class UpdatePropertyAlertCommandHandler : IRequestHandler<UpdatePropertyAlertCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePropertyAlertCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Unit>> Handle(UpdatePropertyAlertCommand request, CancellationToken cancellationToken)
    {
        var alert = await _context.PropertyAlerts
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUserService.UserId, cancellationToken);

        if (alert == null)
            return Result<Unit>.Failure("Alert not found");

        alert.City = request.City;
        alert.District = request.District;
        alert.MaxPrice = request.MaxPrice;
        alert.MinPrice = request.MinPrice;
        alert.MinBedrooms = request.MinBedrooms;
        alert.MinSquareMeters = request.MinSquareMeters;
        alert.Type = request.Type;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
} 