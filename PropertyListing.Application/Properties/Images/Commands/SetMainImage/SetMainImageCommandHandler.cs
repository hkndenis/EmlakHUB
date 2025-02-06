using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Images.Commands.SetMainImage;

public class SetMainImageCommandHandler : IRequestHandler<SetMainImageCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public SetMainImageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(SetMainImageCommand request, CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == request.PropertyId, cancellationToken);

        if (property == null)
            return Result<Unit>.Failure($"Property with ID {request.PropertyId} was not found.");

        var newMainImage = property.Images.FirstOrDefault(i => i.Id == request.ImageId);
        if (newMainImage == null)
            return Result<Unit>.Failure($"Image with ID {request.ImageId} was not found.");

        foreach (var image in property.Images)
        {
            image.IsMain = image.Id == request.ImageId;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
} 