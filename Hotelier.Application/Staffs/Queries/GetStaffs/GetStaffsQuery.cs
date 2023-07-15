using Hotelier.Application.Staffs.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Staffs.Queries.GetStaffs;

public class GetStaffsQuery : IRequest<List<StaffDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
}