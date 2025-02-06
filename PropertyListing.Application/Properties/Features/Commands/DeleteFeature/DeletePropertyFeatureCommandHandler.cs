using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Features.Commands.DeleteFeature;

public class DeletePropertyFeatureCommandHandler : IRequestHandler<DeletePropertyFeatureCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public DeletePropertyFeatureCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(DeletePropertyFeatureCommand request, CancellationToken cancellationToken)
    {
        var feature = await _context.PropertyFeatures
            .FirstOrDefaultAsync(f => f.Id == request.FeatureId && f.PropertyId == request.PropertyId, cancellationToken);

        if (feature == null)
            return Result<Unit>.Failure($"Feature with ID {request.FeatureId} was not found for property {request.PropertyId}.");

        _context.PropertyFeatures.Remove(feature);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
} 