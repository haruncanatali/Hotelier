using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Testimonials.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Testimonials.Queries.GetTestimonials;

public class GetTestimonialsQueryHandler : IRequestHandler<GetTestimonialsQuery,List<TestimonialDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetTestimonialsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TestimonialDto>> Handle(GetTestimonialsQuery request, CancellationToken cancellationToken)
    {
        List<TestimonialDto>? result = await _context.Testimonials
            .Where(c =>
                (request.Description == null || c.Description.ToLower().Contains(request.Description.ToLower()) &&
                    (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower()) &&
                        (request.Type == null || c.Type.ToLower().Contains(request.Type.ToLower())))))
            .OrderByDescending(c=>c.CreatedAt)
            .ProjectTo<TestimonialDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}