using Hotelier.Application.Testimonials.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Testimonials.Queries.GetTestimonials;

public class GetTestimonialsQuery : IRequest<GetTestimonialsVm>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
}