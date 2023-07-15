using FluentValidation;

namespace Hotelier.Application.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
{
    public DeleteRoomCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithMessage("Parametre hatası! - (Id not null - DeleteRoom)");
    }
}