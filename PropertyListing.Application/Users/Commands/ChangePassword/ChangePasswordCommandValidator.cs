using FluentValidation;

namespace PropertyListing.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(v => v.CurrentPassword)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(v => v.NewPassword)
            .NotEmpty()
            .MinimumLength(6)
            .NotEqual(v => v.CurrentPassword)
            .WithMessage("Yeni şifre eski şifre ile aynı olamaz.");
    }
} 