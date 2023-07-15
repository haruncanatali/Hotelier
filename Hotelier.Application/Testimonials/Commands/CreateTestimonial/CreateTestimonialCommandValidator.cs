using FluentValidation;

namespace Hotelier.Application.Testimonials.Commands.CreateTestimonial;

public class CreateTestimonialCommandValidator : AbstractValidator<CreateTestimonialCommand>
{
    public CreateTestimonialCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithMessage("Ad boş olamaz.");
        RuleFor(c => c.Type).NotEmpty()
            .WithMessage("Tip boş olamaz.");
        RuleFor(c => c.Description).NotEmpty()
            .WithMessage("Açıklama boş olamaz.");
        RuleFor(c => c.Image).NotNull()
            .WithMessage("Fotoğraf boş olamaz.");
    }
}