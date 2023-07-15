using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Testimonials.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Testimonials.Queries.GetTestimonial;

public class GetTestimonialQueryHandler : IRequestHandler<GetTestimonialQuery,TestimonialDto>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetTestimonialQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TestimonialDto> Handle(GetTestimonialQuery request, CancellationToken cancellationToken)
    {
        TestimonialDto? entity = await _context.Testimonials
            .Where(c => c.Id == request.Id)
            .ProjectTo<TestimonialDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return entity;
    }
}