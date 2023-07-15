using FluentValidation;

namespace Hotelier.Application.Testimonials.Commands.UpdateTestimonial;

public class UpdateTestimonialCommandValidator : AbstractValidator<UpdateTestimonialCommand>
{
    public UpdateTestimonialCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithMessage("Ad boş olamaz.");
        RuleFor(c => c.Type).NotEmpty()
            .WithMessage("Tip boş olamaz.");
        RuleFor(c => c.Description).NotEmpty()
            .WithMessage("Açıklama boş olamaz.");
        RuleFor(c => c.Id).NotEmpty()
            .WithMessage("Parametre hatası - (Id not empty - Delete Testimonial)");
    }
}