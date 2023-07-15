using AutoMapper;
using Hotelier.Application.Common.Mappings;
using Hotelier.Domain.Entities;

namespace Hotelier.Application.Subscribes.Queries.Dtos;

public class SubscribeDto : IMapFrom<Subscribe>
{
    public string Mail { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Subscribe, SubscribeDto>();
    }
}