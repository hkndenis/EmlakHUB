using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Application.Properties.Favorites.Commands.ToggleFavorite;

public class ToggleFavoriteCommandHandler : IRequestHandler<ToggleFavoriteCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ToggleFavoriteCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<bool>> Handle(ToggleFavoriteCommand request, CancellationToken cancellationToken)
    {
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => 
                f.UserId == _currentUserService.UserId && 
                f.PropertyId == request.PropertyId, 
                cancellationToken);

        if (favorite != null)
        {
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(false); // Favorilerden çıkarıldı
        }

        favorite = new Favorite
        {
            UserId = _currentUserService.UserId,
            PropertyId = request.PropertyId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "system"
        };

        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Success(true); // Favorilere eklendi
    }
} 