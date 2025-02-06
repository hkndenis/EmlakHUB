using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyListing.Application.PropertyAlerts.Commands.CreateAlert;
using PropertyListing.Application.PropertyAlerts.Commands.UpdateAlert;
using PropertyListing.Application.PropertyAlerts.Commands.DeleteAlert;
using PropertyListing.Application.PropertyAlerts.Commands.ToggleAlert;
using PropertyListing.Application.PropertyAlerts.Queries.GetUserAlerts;
using Swashbuckle.AspNetCore.Annotations;

namespace PropertyListing.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PropertyAlertsController : ApiControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Kullanıcının bildirimlerini getirir")]
    public async Task<ActionResult<List<PropertyAlertDto>>> GetAlerts()
    {
        var result = await Mediator.Send(new GetUserAlertsQuery());
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Yeni bildirim oluşturur")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreatePropertyAlertCommand command)
    {
        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Bildirimi günceller")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdatePropertyAlertCommand command)
    {
        if (id != command.Id)
            return BadRequest();
            
        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Bildirimi siler")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeletePropertyAlertCommand(id));
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return NoContent();
    }

    [HttpPost("{id}/toggle")]
    [SwaggerOperation(Summary = "Bildirimi aktif/pasif yapar")]
    public async Task<ActionResult> Toggle(Guid id)
    {
        var result = await Mediator.Send(new TogglePropertyAlertCommand(id));
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return NoContent();
    }
} 