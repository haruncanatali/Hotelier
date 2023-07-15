using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Subscribes.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Subscribes.Queries.GetSubscribes;

public class GetSubscribesQueryHandler : IRequestHandler<GetSubscribesQuery,List<SubscribeDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetSubscribesQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SubscribeDto>> Handle(GetSubscribesQuery request, CancellationToken cancellationToken)
    {
        List<SubscribeDto>? result = await _context.Subscribes
            .OrderByDescending(c=>c.CreatedAt)
            .ProjectTo<SubscribeDto>(_mapper.ConfigurationProvider)
            .Skip((request.Page-1)*request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return result;
    }
}