using Hotelier.Application.Subscribes.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Subscribes.Queries.GetSubscribe;

public class GetSubscribeQuery : IRequest<SubscribeDto>
{
    public long Id { get; set; }
}