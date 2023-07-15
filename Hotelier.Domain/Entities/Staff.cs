namespace Hotelier.Domain.Entities;

public class Staff : BaseEntity
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public string SocialMediaFacebook { get; set; }
    public string SocialMediaInstagram { get; set; }
    public string SocialMediaTwitter { get; set; }
}