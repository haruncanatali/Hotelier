namespace Hotelier.Application.Common.Models;

public class TokenSetting
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int TokenValidityTime { get; set; }
}