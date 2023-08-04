using AutoMapper;
using Hotelier.Application.Common.Mappings;
using Hotelier.Domain.Entities;

namespace Hotelier.Application.Services.Queries.Dtos;

public class ServiceDto : IMapFrom<Service>
{
    public long Id { get; set; }
    public string Icon { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long UpdatedBy { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Service, ServiceDto>();
    }
}