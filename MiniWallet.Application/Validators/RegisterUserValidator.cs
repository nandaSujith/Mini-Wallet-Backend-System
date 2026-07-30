using FluentValidation;
using MiniWallet.Application.DTOs;

namespace MiniWallet.Application.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(x => x.InitialBalance)
            .GreaterThanOrEqualTo(0);
    }
}