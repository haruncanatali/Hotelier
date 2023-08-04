using System.ComponentModel.DataAnnotations;

namespace Hotelier.BackOffice.Models.Service;

public class ServiceAddRequestModel
{
    [Required(ErrorMessage = "* İkon alanı boş olamaz.")]
    public IFormFile Icon { get; set; }
    [Required(ErrorMessage = "* Başlık alanı boş olamaz.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "* Açıklama alanı boş olamaz.")]
    public string Description { get; set; }
}