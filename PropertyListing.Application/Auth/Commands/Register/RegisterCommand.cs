using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Auth.Commands.Register;

public record RegisterCommand : IRequest<Result<string>>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
} 