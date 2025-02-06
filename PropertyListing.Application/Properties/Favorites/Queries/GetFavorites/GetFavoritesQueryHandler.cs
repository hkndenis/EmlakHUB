using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Properties.Favorites.Dtos;

namespace PropertyListing.Application.Properties.Favorites.Queries.GetFavorites;

public class GetFavoritesQueryHandler : IRequestHandler<GetFavoritesQuery, Result<List<FavoriteDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetFavoritesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Result<List<FavoriteDto>>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
    {
        var favorites = await _context.Favorites
            .Include(f => f.Property)
                .ThenInclude(p => p.Images)
            .Include(f => f.Property)
                .ThenInclude(p => p.Features)
            .Where(f => f.UserId == _currentUserService.UserId)
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync(cancellationToken);

        return Result<List<FavoriteDto>>.Success(
            _mapper.Map<List<FavoriteDto>>(favorites));
    }
} 