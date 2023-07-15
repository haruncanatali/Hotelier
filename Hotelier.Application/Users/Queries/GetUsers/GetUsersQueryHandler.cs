using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Users.Queries.Dtos;
using Hotelier.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery,List<UserDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        List<UserDto>? result = await _userManager.Users
            .Where(c =>
                (request.FirstName == null || c.FirstName.ToLower().Contains(request.FirstName.ToLower())) &&
                (request.LastName == null || c.LastName.ToLower().Contains(request.LastName.ToLower())) &&
                (request.Birthdate == null || c.Birthdate == request.Birthdate) &&
                (request.Email == null || c.Email.ToLower().Contains(request.Email.ToLower())))
            .OrderByDescending(c=>c.CreatedAt)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .Skip((request.Page-1)*request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return result;
    }
}