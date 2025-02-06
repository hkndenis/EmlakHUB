using Microsoft.AspNetCore.Mvc;
using PropertyListing.Application.Properties.Features.Commands.CreateFeature;
using PropertyListing.Application.Properties.Features.Commands.UpdateFeature;
using PropertyListing.Application.Properties.Features.Commands.DeleteFeature;
using PropertyListing.Application.Properties.Features.Queries.GetFeatures;
using Swashbuckle.AspNetCore.Annotations;

namespace PropertyListing.API.Controllers;

[ApiController]
[Route("api/properties/{propertyId}/features")]
public class PropertyFeaturesController : ApiControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Emlak özelliklerini listeler", Description = "Bir emlak ilanına ait özellikleri listeler")]
    public async Task<IActionResult> GetFeatures(Guid propertyId)
    {
        var result = await Mediator.Send(new GetPropertyFeaturesQuery(propertyId));
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Yeni özellik ekler", Description = "Emlak ilanına yeni bir özellik ekler")]
    public async Task<IActionResult> Create(Guid propertyId, CreatePropertyFeatureCommand command)
    {
        if (propertyId != command.PropertyId)
            return BadRequest();

        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return CreatedAtAction(nameof(GetFeatures), new { propertyId = command.PropertyId }, result.Data);
    }

    [HttpPut("{featureId}")]
    [SwaggerOperation(Summary = "Özelliği günceller", Description = "Var olan bir özelliği günceller")]
    public async Task<IActionResult> Update(Guid propertyId, Guid featureId, UpdatePropertyFeatureCommand command)
    {
        if (propertyId != command.PropertyId || featureId != command.FeatureId)
            return BadRequest();

        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return NoContent();
    }

    [HttpDelete("{featureId}")]
    [SwaggerOperation(Summary = "Özelliği siler", Description = "Var olan bir özelliği siler")]
    public async Task<IActionResult> Delete(Guid propertyId, Guid featureId)
    {
        var command = new DeletePropertyFeatureCommand { PropertyId = propertyId, FeatureId = featureId };
        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return NoContent();
    }
} 