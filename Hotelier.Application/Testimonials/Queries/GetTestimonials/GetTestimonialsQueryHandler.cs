using System.Globalization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Testimonials.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Testimonials.Queries.GetTestimonials;

public class GetTestimonialsQueryHandler : IRequestHandler<GetTestimonialsQuery,GetTestimonialsVm>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetTestimonialsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetTestimonialsVm> Handle(GetTestimonialsQuery request, CancellationToken cancellationToken)
    {
        int count = await _context.Testimonials.CountAsync(cancellationToken);

        double pageCount = Math.Ceiling((double)count / (double)request.PageSize);

        List<TestimonialDto>? result = await _context.Testimonials
            .Where(c =>
                (request.Description == null || c.Description.ToLower().Contains(request.Description.ToLower()) &&
                    (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower()) &&
                        (request.Type == null || c.Type.ToLower().Contains(request.Type.ToLower())))))
            .OrderByDescending(c=>c.CreatedAt)
            .ProjectTo<TestimonialDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return new GetTestimonialsVm
        {
            Testimonials = result,
            CurrentPage = request.Page,
            Next = request.Page < pageCount,
            Previous = request.Page>1,
            PageCount = int.Parse(pageCount.ToString(CultureInfo.InvariantCulture))
        };
    }
}