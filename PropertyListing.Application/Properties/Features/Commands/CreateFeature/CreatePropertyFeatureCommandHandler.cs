using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Application.Properties.Features.Commands.CreateFeature;

public class CreatePropertyFeatureCommandHandler : IRequestHandler<CreatePropertyFeatureCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreatePropertyFeatureCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreatePropertyFeatureCommand request, CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == request.PropertyId, cancellationToken);

        if (property == null)
            return Result<Guid>.Failure($"Property with ID {request.PropertyId} was not found.");

        var feature = new PropertyFeature
        {
            PropertyId = request.PropertyId,
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "system"
        };

        _context.PropertyFeatures.Add(feature);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(feature.Id);
    }
} 