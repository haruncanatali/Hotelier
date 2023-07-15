using FluentValidation;

namespace Hotelier.Application.Auth.Queries.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(c => c.RefreshToken).NotEmpty().WithMessage("Parametre hatası!");
    }
}