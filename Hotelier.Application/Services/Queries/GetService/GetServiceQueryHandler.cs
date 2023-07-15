using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Services.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Services.Queries.GetService;

public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery,ServiceDto>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetServiceQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceDto> Handle(GetServiceQuery request, CancellationToken cancellationToken)
    {
        ServiceDto? entity = await _context.Services
            .Where(c => c.Id == request.Id)
            .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return entity;
    }
}