using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Services.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Services.Queries.GetServices;

public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, List<ServiceDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetServicesQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ServiceDto>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        List<ServiceDto>? result = await _context.Services
            .Where(c =>
                (request.Title == null || c.Title.ToLower().Contains(request.Title.ToLower())))
            .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}