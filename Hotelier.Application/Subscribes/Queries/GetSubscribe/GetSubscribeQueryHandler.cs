using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Subscribes.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Subscribes.Queries.GetSubscribe;

public class GetSubscribeQueryHandler : IRequestHandler<GetSubscribeQuery,SubscribeDto>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetSubscribeQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SubscribeDto> Handle(GetSubscribeQuery request, CancellationToken cancellationToken)
    {
        SubscribeDto? result = await _context.Subscribes
            .Where(c => c.Id == request.Id)
            .ProjectTo<SubscribeDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}