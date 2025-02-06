using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Commands.DeleteProperty;

public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public DeletePropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (property == null)
            return Result<Unit>.Failure($"Property with ID {request.Id} was not found.");

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
} 