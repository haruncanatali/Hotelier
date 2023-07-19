using System.ComponentModel.DataAnnotations;

namespace Hotelier.BackOffice.Models.User;

public class UserUpdateRequestModel
{
    public long Id { get; set; }
    [Required(ErrorMessage = "* Ad alanı gereklidir.")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "* Soyad alanı gereklidir.")]
    public string LastName { get; set; }
    public IFormFile? ProfilePhoto { get; set; }
    [Required(ErrorMessage = "* Doğum Tarihi alanı gereklidir.")]
    public DateTime Birthdate { get; set; }
}