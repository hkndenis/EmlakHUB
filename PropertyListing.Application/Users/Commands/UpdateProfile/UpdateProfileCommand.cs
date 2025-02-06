using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Users.Commands.UpdateProfile;

public record UpdateProfileCommand : IRequest<Result<Unit>>
{
    public Guid UserId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
} 