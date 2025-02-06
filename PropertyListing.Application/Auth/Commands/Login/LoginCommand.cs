using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<Result<string>>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
} 