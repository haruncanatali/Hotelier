using Hotelier.Application.Services.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Services.Queries.GetServices;

public class GetServicesQuery : IRequest<List<ServiceDto>>
{
    public string? Title { get; set; }
}