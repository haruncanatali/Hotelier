using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Rooms.Queries.Dtos;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Rooms.Queries.GetRoom;

public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery,RoomDto>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetRoomQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RoomDto> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        RoomDto? entity = await _context.Rooms
            .Where(c => c.Id == request.Id)
            .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return entity;
    }
}