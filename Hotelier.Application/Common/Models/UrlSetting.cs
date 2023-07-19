namespace Hotelier.Application.Common.Models;

public class UrlSetting
{
    public string LocalKey { get; set; }
    public string HostKey { get; set; }
    
    public string LoginPath { get; set; }
    public string UsersPath { get; set; }
    public string UserPath { get; set; }

    public string StaffPath { get; set; }
}