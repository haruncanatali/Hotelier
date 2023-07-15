using FluentValidation;

namespace Hotelier.Application.Services.Commands.CreateService;

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty()
            .WithMessage("Başlık boş olmamalıdır.");
        RuleFor(c => c.Description).NotEmpty()
            .WithMessage("Açıklama boş olmamalıdır.");
        RuleFor(c => c.Icon).NotNull()
            .WithMessage("İkon boş olmamalıdır.");
    }
}