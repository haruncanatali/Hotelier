using FluentValidation;

namespace Hotelier.Application.Testimonials.Commands.DeleteTestimonial;

public class DeleteTestimonialCommandValidator : AbstractValidator<DeleteTestimonialCommand>
{
    public DeleteTestimonialCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty()
            .WithMessage("Parametre hatası - (Id not empty - Delete Testimonial)");
    }
}