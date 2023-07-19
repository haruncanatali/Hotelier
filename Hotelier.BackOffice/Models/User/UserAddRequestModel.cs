using System.ComponentModel.DataAnnotations;

namespace Hotelier.BackOffice.Models.User;

public class UserAddRequestModel
{
    [Required(ErrorMessage = "* Ad alanı gereklidir.")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "* Soyad alanı gereklidir.")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "* E-Posta alanı gereklidir.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "* Şifre alanı gereklidir."),MinLength(6,ErrorMessage= "* Şifre alanı min. 6 haneli olmalıdır.")]
    public string Password { get; set; }
    [Required(ErrorMessage = "* Adres alanı gereklidir.")]
    public string Address { get; set; }
    [Required(ErrorMessage = "* Doğum Tarihi alanı gereklidir.")]
    public DateTime Birthdate { get; set; }
    [Required(ErrorMessage = "* Profil fotoğrafı alanı gereklidir.")]
    public IFormFile ProfilePhoto { get; set; }
    [Required(ErrorMessage = "* Rol adı alanı gereklidir.")]
    public string RoleName { get; set; }
}