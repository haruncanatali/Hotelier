using Hotelier.Application.Staffs.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Staffs.Queries.GetStaff;

public class GetStaffQuery : IRequest<StaffDto>
{
    public long Id { get; set; }
}