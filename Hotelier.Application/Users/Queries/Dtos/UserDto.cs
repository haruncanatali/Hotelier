using AutoMapper;
using Hotelier.Application.Common.Mappings;
using Hotelier.Domain.IdentityEntities;

namespace Hotelier.Application.Users.Queries.Dtos;

public class UserDto : IMapFrom<User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePhoto { get; set; }
    public DateTime Birthdate { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>();
    }
}