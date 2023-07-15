using Hotelier.Application.Services.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Services.Queries.GetService;

public class GetServiceQuery : IRequest<ServiceDto>
{
    public long Id { get; set; }
}