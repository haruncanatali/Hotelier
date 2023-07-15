using AutoMapper;
using Hotelier.Application.Common.Mappings;
using Hotelier.Domain.Entities;

namespace Hotelier.Application.Rooms.Queries.Dtos;

public class RoomDto : IMapFrom<Room>
{
    public string CoverImage { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string RoomNumber { get; set; }
    public decimal Price { get; set; }
    public int BedCount { get; set; }
    public int BathCount { get; set; }
    public bool WiFi { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Room, RoomDto>();
    }
}