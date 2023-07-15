using FluentValidation;

namespace Hotelier.Application.Auth.Queries.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email giriniz.");
        RuleFor(c => c.Password).NotEmpty().WithMessage("Şifre giriniz.");
    }
}