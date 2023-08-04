using Hotelier.Application.Services.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Services.Queries.GetServices;

public class GetServicesQuery : IRequest<GetServicesVm>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Title { get; set; }
}