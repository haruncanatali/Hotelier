using FluentValidation;

namespace Hotelier.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(c => c.CoverImage).NotNull()
            .WithMessage("Fotoğraf boş olmamalıdır.");
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