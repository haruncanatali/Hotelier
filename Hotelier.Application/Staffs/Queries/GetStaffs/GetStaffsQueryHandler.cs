using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Staffs.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Staffs.Queries.GetStaffs;

public class GetStaffsQueryHandler : IRequestHandler<GetStaffsQuery,List<StaffDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetStaffsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<StaffDto>> Handle(GetStaffsQuery request, CancellationToken cancellationToken)
    {
        List<StaffDto>? result = await _context.Staffs
            .Where(c =>
                (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())) &&
                (request.Title == null || c.Title.ToLower().Contains(request.Title.ToLower())))
            .OrderByDescending(c=>c.CreatedAt)
            .ProjectTo<StaffDto>(_mapper.ConfigurationProvider)
            .Skip((request.Page-1)*request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return result;
    }
}