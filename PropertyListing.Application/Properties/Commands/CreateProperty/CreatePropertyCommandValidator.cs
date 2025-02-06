using FluentValidation;

namespace PropertyListing.Application.Properties.Commands.CreateProperty;

public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(v => v.Price)
            .GreaterThan(0);

        RuleFor(v => v.Currency)
            .NotEmpty()
            .Length(3);

        RuleFor(v => v.City)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.District)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.Street)
            .NotEmpty()
            .MaximumLength(200);
    }
} 