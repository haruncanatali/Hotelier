using System.Globalization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Subscribes.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Subscribes.Queries.GetSubscribes;

public class GetSubscribesQueryHandler : IRequestHandler<GetSubscribesQuery,GetSubscribesVm>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetSubscribesQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetSubscribesVm> Handle(GetSubscribesQuery request, CancellationToken cancellationToken)
    {
        int count = await _context.Subscribes.CountAsync(cancellationToken);

        double pageCount = Math.Ceiling((double)count / (double)request.PageSize);
        
        List<SubscribeDto>? result = await _context.Subscribes
            .OrderByDescending(c=>c.CreatedAt)
            .ProjectTo<SubscribeDto>(_mapper.ConfigurationProvider)
            .Skip((request.Page-1)*request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        
        return new GetSubscribesVm
        {
            Subscribes = result,
            CurrentPage = request.Page,
            Next = request.Page < pageCount,
            Previous = request.Page>1,
            PageCount = int.Parse(pageCount.ToString(CultureInfo.InvariantCulture))
        };
    }
}