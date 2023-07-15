using Hotelier.Application.Subscribes.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Subscribes.Queries.GetSubscribes;

public class GetSubscribesQuery : IRequest<List<SubscribeDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}