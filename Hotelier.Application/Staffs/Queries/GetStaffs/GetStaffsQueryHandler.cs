using System.Globalization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Staffs.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Staffs.Queries.GetStaffs;

public class GetStaffsQueryHandler : IRequestHandler<GetStaffsQuery,GetStaffsVm>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetStaffsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetStaffsVm> Handle(GetStaffsQuery request, CancellationToken cancellationToken)
    {
        int count = await _context.Staffs.CountAsync(cancellationToken);

        double pageCount = Math.Ceiling((double)count / (double)request.PageSize);
        
        List<StaffDto>? result = await _context.Staffs
            .Where(c =>
                (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())) &&
                (request.Title == null || c.Title.ToLower().Contains(request.Title.ToLower())))
            .OrderByDescending(c=>c.CreatedAt)
            .ProjectTo<StaffDto>(_mapper.ConfigurationProvider)
            .Skip((request.Page-1)*request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        
        return new GetStaffsVm
        {
            Staffs = result,
            CurrentPage = request.Page,
            Next = request.Page < pageCount,
            Previous = request.Page>1,
            PageCount = int.Parse(pageCount.ToString(CultureInfo.InvariantCulture))
        };
    }
}