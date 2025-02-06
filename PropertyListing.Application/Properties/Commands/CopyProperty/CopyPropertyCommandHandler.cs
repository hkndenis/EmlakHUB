using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Entities;
using PropertyListing.Domain.Enums;
using PropertyListing.Domain.ValueObjects;  // Money için

namespace PropertyListing.Application.Properties.Commands.CopyProperty;

public class CopyPropertyCommandHandler : IRequestHandler<CopyPropertyCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CopyPropertyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> Handle(CopyPropertyCommand request, CancellationToken cancellationToken)
    {
        var sourceProperty = await _context.Properties
            .Include(p => p.Location)
            .FirstOrDefaultAsync(p => p.Id == request.SourcePropertyId, cancellationToken);

        if (sourceProperty == null)
            return Result<Guid>.Failure($"Source property with ID {request.SourcePropertyId} was not found.");

        var newProperty = new Property
        {
            Title = sourceProperty.Title,
            Description = sourceProperty.Description,
            Price = request.NewPrice.HasValue 
                ? Money.FromDecimal(request.NewPrice.Value)
                : sourceProperty.Price,
            Location = new PropertyLocation
            {
                City = sourceProperty.Location.City,
                District = sourceProperty.Location.District,
                PostalCode = sourceProperty.Location.PostalCode,
                Latitude = sourceProperty.Location.Latitude,
                Longitude = sourceProperty.Location.Longitude
            },
            Type = request.NewType,
            Status = PropertyStatus.Available,
            CreatedBy = _currentUserService.UserId.ToString(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Properties.Add(newProperty);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(newProperty.Id);
    }
} 