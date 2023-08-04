using System.ComponentModel.DataAnnotations;

namespace Hotelier.BackOffice.Models.Service;

public class ServiceUpdateRequestModel
{
    [Required(ErrorMessage = "* Id alanı boş olamaz.")]
    public long Id { get; set; }
    public IFormFile? Icon { get; set; }
    [Required(ErrorMessage = "* Başlık alanı boş olamaz.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "* Açıklama alanı boş olamaz.")]
    public string Description { get; set; }
}