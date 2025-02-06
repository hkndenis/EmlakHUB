using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyListing.API.Extensions;
using PropertyListing.Application.Properties.Favorites.Commands.ToggleFavorite;
using PropertyListing.Application.Properties.Favorites.Queries.GetFavorites;
using Swashbuckle.AspNetCore.Annotations;

namespace PropertyListing.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ApiControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Favori ilanları listeler", Description = "Kullanıcının favori ilanlarını listeler")]
    public async Task<IActionResult> GetFavorites()
    {
        var result = await Mediator.Send(new GetFavoritesQuery(User.GetUserId()));
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }

    [HttpPost("{propertyId}")]
    [SwaggerOperation(Summary = "Favorilere ekler/çıkarır", Description = "İlanı favorilere ekler veya favorilerden çıkarır")]
    public async Task<IActionResult> ToggleFavorite(Guid propertyId)
    {
        var command = new ToggleFavoriteCommand
        {
            UserId = User.GetUserId(),
            PropertyId = propertyId
        };

        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }
} 