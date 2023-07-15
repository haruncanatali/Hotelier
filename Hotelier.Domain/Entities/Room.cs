namespace Hotelier.Domain.Entities;

public class Room : BaseEntity
{
    public string CoverImage { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string RoomNumber { get; set; }

    public decimal Price { get; set; }

    public int BedCount { get; set; }
    public int BathCount { get; set; }

    public bool WiFi { get; set; }
}