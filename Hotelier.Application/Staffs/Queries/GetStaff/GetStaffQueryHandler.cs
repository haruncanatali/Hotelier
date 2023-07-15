using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Staffs.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Staffs.Queries.GetStaff;

public class GetStaffQueryHandler : IRequestHandler<GetStaffQuery,StaffDto>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetStaffQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<StaffDto> Handle(GetStaffQuery request, CancellationToken cancellationToken)
    {
        StaffDto? entity = await _context.Staffs
            .Where(c => c.Id == request.Id)
            .ProjectTo<StaffDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return entity;
    }
}