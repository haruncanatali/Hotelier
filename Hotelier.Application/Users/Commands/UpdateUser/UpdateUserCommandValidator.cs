using FluentValidation;

namespace Hotelier.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().WithName("Ad boş olmamalıdır.");
        RuleFor(c => c.LastName).NotEmpty().WithName("Soyad boş olmamalıdır.");
        RuleFor(c => c.Birthdate).NotEmpty().WithName("Doğum tarihi boş olmamalıdır.");
    }
}