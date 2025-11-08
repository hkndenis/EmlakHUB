using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace PropertyListing.API.Controllers;

/// <summary>
/// Tüm API controller'ları için temel sınıf. MediatR entegrasyonu sağlar.
/// Base class for all API controllers. Provides MediatR integration.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    /// <summary>
    /// MediatR sender instance'ı. Command ve query'leri göndermek için kullanılır.
    /// MediatR sender instance. Used to send commands and queries.
    /// </summary>
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
} 