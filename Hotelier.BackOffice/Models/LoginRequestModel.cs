using System.ComponentModel.DataAnnotations;

namespace Hotelier.BackOffice.Models;

public class LoginRequestModel
{
    [Required(ErrorMessage = "* E-Posta alanı gereklidir.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "* Parola alanı gereklidir.")]
    public string Password { get; set; }
}