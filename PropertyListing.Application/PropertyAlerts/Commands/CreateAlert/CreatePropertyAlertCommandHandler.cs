using MediatR;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Application.PropertyAlerts.Commands.CreateAlert;

public class CreatePropertyAlertCommandHandler : IRequestHandler<CreatePropertyAlertCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreatePropertyAlertCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> Handle(CreatePropertyAlertCommand request, CancellationToken cancellationToken)
    {
        var alert = new PropertyAlert
        {
            UserId = _currentUserService.UserId,
            City = request.City,
            District = request.District,
            MaxPrice = request.MaxPrice,
            MinPrice = request.MinPrice,
            MinBedrooms = request.MinBedrooms,
            MinSquareMeters = request.MinSquareMeters,
            Type = request.Type,
            LastNotificationSent = DateTime.UtcNow
        };

        _context.PropertyAlerts.Add(alert);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(alert.Id);
    }
} 