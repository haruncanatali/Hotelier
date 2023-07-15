using Hotelier.Application.Testimonials.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Testimonials.Queries.GetTestimonials;

public class GetTestimonialsQuery : IRequest<List<TestimonialDto>>
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
}