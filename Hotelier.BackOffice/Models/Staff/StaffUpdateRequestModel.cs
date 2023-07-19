using System.ComponentModel.DataAnnotations;

namespace Hotelier.BackOffice.Models.Staff;

public class StaffUpdateRequestModel
{
    [Required(ErrorMessage = "* Id alanı boş olamaz.")]
    public long Id { get; set; }
    [Required(ErrorMessage = "* Ad alanı boş olamaz.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "* Unvan alanı boş olamaz.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "* Facebook alanı boş olamaz.")]
    public string SocialMediaFacebook { get; set; }
    [Required(ErrorMessage = "* Instagram alanı boş olamaz.")]
    public string SocialMediaInstagram { get; set; }
    [Required(ErrorMessage = "* Twitter alanı boş olamaz.")]
    public string SocialMediaTwitter { get; set; }
    public IFormFile? Image { get; set; }
}