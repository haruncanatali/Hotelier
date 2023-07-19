using AutoMapper;
using Hotelier.Application.Common.Mappings;
using Hotelier.Domain.Entities;

namespace Hotelier.Application.Staffs.Queries.Dtos;

public class StaffDto : IMapFrom<Staff>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public string SocialMediaFacebook { get; set; }
    public string SocialMediaInstagram { get; set; }
    public string SocialMediaTwitter { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long UpdatedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Staff, StaffDto>();
    }
}