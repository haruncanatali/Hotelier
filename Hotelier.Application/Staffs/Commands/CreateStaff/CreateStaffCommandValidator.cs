using FluentValidation;

namespace Hotelier.Application.Staffs.Commands.CreateStaff;

public class CreateStaffCommandValidator : AbstractValidator<CreateStaffCommand>
{
    public CreateStaffCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithMessage("Ad boş olmamalıdır.");
        RuleFor(c => c.Title).NotEmpty()
            .WithMessage("Başlık boş olmamalıdır.");
        RuleFor(c => c.SocialMediaFacebook).NotEmpty()
            .WithMessage("Facebook bağlantısı boş olmamalıdır.");
        RuleFor(c => c.SocialMediaTwitter).NotEmpty()
            .WithMessage("Twitter bağlantısı boş olmamalıdır.");
        RuleFor(c => c.SocialMediaInstagram).NotEmpty()
            .WithMessage("Instagram bağlantısı boş olmamalıdır.");
        RuleFor(c => c.Image).NotNull()
            .WithMessage("Fotoğraf boş olmamalıdır.");
    }
}