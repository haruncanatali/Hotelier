using FluentValidation;

namespace Hotelier.Application.Subscribes.Commands.CreateSubscribe;

public class CreateSubscribeCommandValidator : AbstractValidator<CreateSubscribeCommand>
{
    public CreateSubscribeCommandValidator()
    {
        RuleFor(c => c.Mail).NotEmpty().WithMessage("Mail boş olmamalıdır.");
    }
}