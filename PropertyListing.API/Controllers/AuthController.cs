using Microsoft.AspNetCore.Mvc;
using PropertyListing.Application.Auth.Commands.Login;
using PropertyListing.Application.Auth.Commands.Register;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Infrastructure.Persistence;
using PropertyListing.Application.Common.Interfaces;

namespace PropertyListing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ApiControllerBase
{
    private readonly IApplicationDbContext _context;

    public AuthController(IApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    [SwaggerOperation(Summary = "Yeni kullanıcı kaydı", Description = "Sisteme yeni bir kullanıcı kaydeder")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
            
        // Kullanıcı bilgilerini de döndür
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == command.Email);
        return Ok(new { 
            token = result.Data,
            user = new {
                id = user.Id,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName
            }
        });
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Kullanıcı girişi", Description = "Kullanıcı girişi yapar ve JWT token döner")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
            
        // Kullanıcı bilgilerini de döndür
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == command.Email);
        return Ok(new { 
            token = result.Data,
            user = new {
                id = user.Id,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName
            }
        });
    }
} 