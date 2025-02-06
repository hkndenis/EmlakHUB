using Microsoft.AspNetCore.Mvc;
using PropertyListing.Application.Properties.Images.Commands.UploadImage;
using PropertyListing.Application.Properties.Images.Commands.SetMainImage;
using PropertyListing.Application.Properties.Images.Commands.DeleteImage;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace PropertyListing.API.Controllers;

[Authorize]
[ApiController]
[Route("api/properties/{propertyId}/images")]
public class PropertyImagesController : ApiControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Resim yükler", Description = "Emlak ilanına yeni bir resim yükler")]
    public async Task<IActionResult> Upload(Guid propertyId, IFormFile image, [FromForm] bool isMain = false)
    {
        var command = new UploadPropertyImageCommand
        {
            PropertyId = propertyId,
            Image = image,
            IsMain = isMain
        };

        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }

    [HttpPut("{imageId}/set-main")]
    [SwaggerOperation(Summary = "Ana resmi belirler", Description = "Belirtilen resmi ana resim olarak ayarlar")]
    public async Task<IActionResult> SetMainImage(Guid propertyId, Guid imageId)
    {
        var command = new SetMainImageCommand
        {
            PropertyId = propertyId,
            ImageId = imageId
        };

        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Fotoğraf sil",
        Description = "İlana ait bir fotoğrafı siler")]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        var command = new DeletePropertyImageCommand
        {
            ImageId = id  // Artık sadece ImageId kullanıyoruz, PropertyId yok
        };

        var result = await Mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok();
    }
} 