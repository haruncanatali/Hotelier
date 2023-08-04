using Hotelier.Application.Subscribes.Queries.Dtos;

namespace Hotelier.Application.Subscribes.Queries.GetSubscribes;

public class GetSubscribesVm
{
    public List<SubscribeDto> Subscribes { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public bool Next { get; set; }
    public bool Previous { get; set; }
}