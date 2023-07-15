using Hotelier.Application.Testimonials.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Testimonials.Queries.GetTestimonial;

public class GetTestimonialQuery : IRequest<TestimonialDto>
{
    public long Id { get; set; }
}