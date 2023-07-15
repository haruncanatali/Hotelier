using Hotelier.Application.Rooms.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Rooms.Queries.GetRoom;

public class GetRoomQuery : IRequest<RoomDto>
{
    public long Id { get; set; }
}