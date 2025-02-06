using MediatR;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (!await _identityService.IsEmailUniqueAsync(request.Email))
        {
            return Result<string>.Failure("Bu email adresi zaten kullanımda");
        }

        return await _identityService.RegisterAsync(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName);
    }
} 