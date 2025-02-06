using MediatR;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Entities;
using PropertyListing.Domain.Enums;
using PropertyListing.Domain.ValueObjects;

namespace PropertyListing.Application.Properties.Commands.CreateProperty;

public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreatePropertyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = new Property
        {
            Title = request.Title,
            Description = request.Description,
            Price = Money.FromDecimal(request.Price),
            Location = PropertyLocation.FromAddress(new Address(
                request.Street,
                request.District,
                request.City,
                request.PostalCode,
                request.Latitude,
                request.Longitude)),
            Bedrooms = (int)request.Bedrooms,
            Bathrooms = (int)request.Bathrooms,
            SquareMeters = (int)request.SquareMeters,
            Type = request.Type,
            Status = PropertyStatus.Available,
            CreatedBy = _currentUserService.UserId.ToString(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Properties.Add(property);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(property.Id);
    }
} 