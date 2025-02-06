using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyListing.API.Extensions;
using PropertyListing.Application.Users.Commands.UpdateProfile;
using PropertyListing.Application.Users.Commands.ChangePassword;
using PropertyListing.Application.Users.Queries.GetProfile;
using Swashbuckle.AspNetCore.Annotations;

namespace PropertyListing.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ApiControllerBase
{
    [HttpGet("profile")]
    [SwaggerOperation(Summary = "Kullanıcı profilini getirir", Description = "Giriş yapmış kullanıcının profil bilgilerini getirir")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.GetUserId();
        var result = await Mediator.Send(new GetProfileQuery(userId));
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }

    [HttpPut("profile")]
    [SwaggerOperation(Summary = "Profil günceller", Description = "Kullanıcı profil bilgilerini günceller")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileCommand command)
    {
        command = command with { UserId = User.GetUserId() };
        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return NoContent();
    }

    [HttpPost("change-password")]
    [SwaggerOperation(Summary = "Şifre değiştirir", Description = "Kullanıcının şifresini değiştirir")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        command = command with { UserId = User.GetUserId() };
        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return NoContent();
    }
} 