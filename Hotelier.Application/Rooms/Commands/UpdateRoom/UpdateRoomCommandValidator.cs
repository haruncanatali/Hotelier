using FluentValidation;

namespace Hotelier.Application.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithMessage("Parametre hatası - (Id not null - Update Room).");
        RuleFor(c => c.Title).NotEmpty()
            .WithMessage("Başlık boş olmamalıdır.");
        RuleFor(c => c.Description).NotEmpty()
            .WithMessage("Açıklama boş olmamalıdır.");
        RuleFor(c => c.RoomNumber).NotEmpty()
            .WithMessage("Oda numarası boş olmamalıdır.");
        RuleFor(c => c.Price).NotNull()
            .WithMessage("Fiyat boş olmamalıdır.");
        RuleFor(c => c.BathCount).NotNull()
            .WithMessage("Banyo sayısı boş olmamalıdır.");
        RuleFor(c => c.BedCount).NotNull()
            .WithMessage("Yatak sayısı boş olmamalıdır.");
        RuleFor(c => c.WiFi).NotNull()
            .WithMessage("WiFi özelliği boş olmamalıdır.");
    }
}