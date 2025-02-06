using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;  // Result için

namespace PropertyListing.Application.Properties.Commands.IncrementViewCount
{
    public class IncrementViewCountCommandHandler : IRequestHandler<IncrementViewCountCommand, Result<Unit>>
    {
        private readonly IApplicationDbContext _context;

        public IncrementViewCountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(IncrementViewCountCommand request, CancellationToken cancellationToken)
        {
            var property = await _context.Properties
                .FirstOrDefaultAsync(p => p.Id == request.PropertyId, cancellationToken);

            if (property == null)
                return Result<Unit>.Failure($"Property with ID {request.PropertyId} was not found.");

            property.ViewCount++;
            property.LastViewedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }
} 