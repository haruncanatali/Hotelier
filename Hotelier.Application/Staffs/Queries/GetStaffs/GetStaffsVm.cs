using Hotelier.Application.Staffs.Queries.Dtos;

namespace Hotelier.Application.Staffs.Queries.GetStaffs;

public class GetStaffsVm
{
    public List<StaffDto> Staffs { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public bool Next { get; set; }
    public bool Previous { get; set; }
}