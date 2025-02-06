using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Users.Commands.ChangePassword;

public record ChangePasswordCommand : IRequest<Result<Unit>>
{
    public Guid UserId { get; init; }
    public string CurrentPassword { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
} 