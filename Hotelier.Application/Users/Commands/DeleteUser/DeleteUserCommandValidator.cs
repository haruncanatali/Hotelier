using FluentValidation;

namespace Hotelier.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().WithMessage("Parametre hatası! (Delete-UserId null!)");
    }
}