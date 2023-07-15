using Hotelier.Application.Rooms.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Rooms.Queries.GetRooms;

public class GetRoomsQuery : IRequest<List<RoomDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Title { get; set; }
    public string? RoomNumber { get; set; }
    public decimal? Price { get; set; }
    public int? BedCount { get; set; }
    public int? BathCount { get; set; }
    public bool? WiFi { get; set; }
}