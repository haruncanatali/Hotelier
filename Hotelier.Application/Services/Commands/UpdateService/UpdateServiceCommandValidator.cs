using FluentValidation;

namespace Hotelier.Application.Services.Commands.UpdateService;

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithMessage("Parametre hatası! - (Id not null - Update Service");
        RuleFor(c => c.Title).NotEmpty()
            .WithMessage("Başlık boş olmamalıdır.");
        RuleFor(c => c.Description).NotEmpty()
            .WithMessage("Açıklama boş olmamalıdır.");
    }
}