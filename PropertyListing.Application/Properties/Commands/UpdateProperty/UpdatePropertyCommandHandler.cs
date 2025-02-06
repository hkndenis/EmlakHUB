using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Entities;
using PropertyListing.Domain.ValueObjects;

namespace PropertyListing.Application.Properties.Commands.UpdateProperty;

public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public UpdatePropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .Include(p => p.Location)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (property == null)
            return Result<Unit>.Failure($"Property with ID {request.Id} was not found.");

        property.Title = request.Title;
        property.Description = request.Description;
        property.Price = Money.FromDecimal(request.Price);
        property.Location = PropertyLocation.FromAddress(new Address(
            request.Street,
            request.District,
            request.City,
            request.PostalCode,
            request.Latitude,
            request.Longitude));
        property.Bedrooms = (int)request.Bedrooms;
        property.Bathrooms = (int)request.Bathrooms;
        property.SquareMeters = (int)request.SquareMeters;
        property.LastModifiedAt = DateTime.UtcNow;
        property.LastModifiedBy = "system"; // Daha sonra gerçek kullanıcı bilgisi eklenecek

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
} 