using System.ComponentModel.DataAnnotations;

namespace Hotelier.BackOffice.Models.Room;

public class RoomUpdateRequestModel
{
    [Required(ErrorMessage = "* Id boş olmamalıdır.")]
    public long Id { get; set; }
    public IFormFile? CoverImage { get; set; }
    [Required(ErrorMessage = "* Başlık boş olmamalıdır.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "* Açıklama boş olmamalıdır.")]
    public string Description { get; set; }
    [Required(ErrorMessage = "* Oda numarası boş olmamalıdır.")]
    public string RoomNumber { get; set; }
    [Required(ErrorMessage = "* Ücret boş olmamalıdır.")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "* Yatak Sayısı boş olmamalıdır.")]
    public int BedCount { get; set; }
    [Required(ErrorMessage = "* Banyo Sayısı boş olmamalıdır.")]
    public int BathCount { get; set; }
    [Required(ErrorMessage = "* WiFi boş olmamalıdır.")]
    public bool WiFi { get; set; }
}