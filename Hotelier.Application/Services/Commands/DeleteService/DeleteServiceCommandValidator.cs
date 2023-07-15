using FluentValidation;

namespace Hotelier.Application.Services.Commands.DeleteService;

public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
    public DeleteServiceCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithMessage("Parametre hatası! - (Id not null - Delete Service)");
    }
}