using AutoMapper;
using Hotelier.Application.Common.Mappings;
using Hotelier.Domain.Entities;

namespace Hotelier.Application.Testimonials.Queries.Dtos;

public class TestimonialDto : IMapFrom<Testimonial>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public long CreatedBy { get; set; }
    public long UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Testimonial, TestimonialDto>();
    }
}