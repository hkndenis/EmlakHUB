using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Entities;
using PropertyListing.Domain.Enums;  // PropertyStatus için

namespace PropertyListing.Application.Properties.Commands.UpdateStatus;

public class UpdatePropertyStatusCommandHandler : IRequestHandler<UpdatePropertyStatusCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePropertyStatusCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Unit>> Handle(UpdatePropertyStatusCommand request, CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == request.PropertyId, cancellationToken);

        if (property == null)
            return Result<Unit>.Failure($"Property with ID {request.PropertyId} was not found.");

        var oldStatus = property.Status;
        property.Status = request.Status;
        property.LastModifiedAt = DateTime.UtcNow;
        property.LastModifiedBy = "system";

        var statusHistory = new PropertyStatusHistory
        {
            PropertyId = property.Id,
            Status = request.Status,
            Note = request.Note,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = _currentUserService.UserId.ToString()
        };

        _context.PropertyStatusHistory.Add(statusHistory);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
} 