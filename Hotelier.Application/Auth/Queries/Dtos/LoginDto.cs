namespace Hotelier.Application.Auth.Queries.Dtos;

public class LoginDto
{
    public User User { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class User
{
    public string Role { get; set; }
    public long Id { get; set; }
    public string Email { get; set; }
    public string Firstname { get; set; }
    public string LastName { get; set; }
    public string Photo { get; set; }
}