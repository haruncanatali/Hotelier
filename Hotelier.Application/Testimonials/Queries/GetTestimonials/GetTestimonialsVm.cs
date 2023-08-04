using Hotelier.Application.Testimonials.Queries.Dtos;

namespace Hotelier.Application.Testimonials.Queries.GetTestimonials;

public class GetTestimonialsVm
{
    public List<TestimonialDto> Testimonials { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public bool Next { get; set; }
    public bool Previous { get; set; }
}