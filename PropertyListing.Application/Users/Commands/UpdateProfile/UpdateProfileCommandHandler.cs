using MediatR;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Users.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<Unit>>
{
    private readonly IIdentityService _identityService;

    public UpdateProfileCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<Unit>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.UpdateProfileAsync(
            request.UserId,
            request.FirstName,
            request.LastName,
            request.PhoneNumber);
    }
} 