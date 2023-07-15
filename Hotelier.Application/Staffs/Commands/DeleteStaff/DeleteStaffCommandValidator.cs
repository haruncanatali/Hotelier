using FluentValidation;

namespace Hotelier.Application.Staffs.Commands.DeleteStaff;

public class DeleteStaffCommandValidator : AbstractValidator<DeleteStaffCommand>
{
    public DeleteStaffCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithMessage("Parametre hatası - (Id not null - Delete Staff)");
    }
}