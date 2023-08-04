using System.Globalization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Services.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Services.Queries.GetServices;

public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, GetServicesVm>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetServicesQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetServicesVm> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        int count = await _context.Services.CountAsync(cancellationToken);

        double pageCount = Math.Ceiling((double)count / (double)request.PageSize);
        
        
        List<ServiceDto>? result = await _context.Services
            .Where(c =>
                (request.Title == null || c.Title.ToLower().Contains(request.Title.ToLower())))
            .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetServicesVm
        {
            Services = result,
            CurrentPage = request.Page,
            Next = request.Page < pageCount,
            Previous = request.Page>1,
            PageCount = int.Parse(pageCount.ToString(CultureInfo.InvariantCulture))
        };
    }
}