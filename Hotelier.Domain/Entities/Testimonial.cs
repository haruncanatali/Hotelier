namespace Hotelier.Domain.Entities;

public class Testimonial : BaseEntity
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
}