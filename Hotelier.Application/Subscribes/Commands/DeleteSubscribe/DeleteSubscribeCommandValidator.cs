using FluentValidation;

namespace Hotelier.Application.Subscribes.Commands.DeleteSubscribe;

public class DeleteSubscribeCommandValidator : AbstractValidator<DeleteSubscribeCommand>
{
    public DeleteSubscribeCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().WithMessage("Parametre hatası => (ID Not Null!-DeleteSubscribe)");
    }
}