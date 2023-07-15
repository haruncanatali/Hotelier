using FluentValidation;

namespace Hotelier.Application.Staffs.Commands.UpdateStaff;

public class UpdateStaffCommandValidator : AbstractValidator<UpdateStaffCommand>
{
    public UpdateStaffCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithMessage("Parametre hatası! - (Id not null! - Update Staff)");
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
    }
}