using Hotelier.Application.Services.Queries.Dtos;

namespace Hotelier.Application.Services.Queries.GetServices;

public class GetServicesVm
{
    public List<ServiceDto> Services { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public bool Next { get; set; }
    public bool Previous { get; set; }
}