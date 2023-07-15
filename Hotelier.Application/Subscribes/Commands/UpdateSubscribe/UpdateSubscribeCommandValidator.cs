using FluentValidation;

namespace Hotelier.Application.Subscribes.Commands.UpdateSubscribe;

public class UpdateSubscribeCommandValidator : AbstractValidator<UpdateSubscribeCommand>
{
    public UpdateSubscribeCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().WithMessage("Parametre hatası - (Id not null!-UpdateSubscribe)");
        RuleFor(c => c.Mail).NotEmpty().WithMessage("Mail boş olamaz");
    }
}