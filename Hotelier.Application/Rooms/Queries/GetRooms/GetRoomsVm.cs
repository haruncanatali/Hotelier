using Hotelier.Application.Rooms.Queries.Dtos;

namespace Hotelier.Application.Rooms.Queries.GetRooms;

public class GetRoomsVm
{
    public List<RoomDto> Rooms { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public bool Next { get; set; }
    public bool Previous { get; set; }
}