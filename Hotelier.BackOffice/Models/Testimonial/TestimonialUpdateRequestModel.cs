using System.ComponentModel.DataAnnotations;

namespace Hotelier.BackOffice.Models.Testimonial;

public class TestimonialUpdateRequestModel
{
    [Required(ErrorMessage = "* Id alanı gereklidir.")]
    public long Id { get; set; }
    [Required(ErrorMessage = "* Ad soyad alanı gereklidir.")]
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
    [Required(ErrorMessage = "* Tip alanı gereklidir.")]
    public string Type { get; set; }
    [Required(ErrorMessage = "* Açıklama alanı gereklidir.")]
    public string Description { get; set; }
}