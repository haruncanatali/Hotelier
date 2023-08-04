using Hotelier.Application.Subscribes.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Subscribes.Queries.GetSubscribes;

public class GetSubscribesQuery : IRequest<GetSubscribesVm>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}