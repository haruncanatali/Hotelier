using AutoMapper;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Users.Queries.Dtos;
using Hotelier.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hotelier.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery,UserDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        UserDto result = null;

        User? user = await _userManager.FindByIdAsync(request.Id.ToString());

        if (user != null)
        {
            result = _mapper.Map<UserDto>(user);
        }

        return result;
    }
}