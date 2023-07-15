using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Hotelier.Domain.IdentityEntities;

public class User : IdentityUser<long>
{
    public User()
    {
        CreatedAt = DateTime.Now;
    }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePhoto { get; set; }
    public DateTime Birthdate { get; set; }
    public string Address { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiredTime { get; set; }

    public DateTime CreatedAt { get; set; }


    [IgnoreDataMember]
    public string FullName
    {
        get { return $"{FirstName} {LastName}"; }
    }
}