using System.Globalization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Users.Queries.Dtos;
using Hotelier.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery,GetUsersVm>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<GetUsersVm> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        int count = _userManager.Users.Count();

        double pageCount = Math.Ceiling((double)count / (double)request.PageSize);
        
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

        return new GetUsersVm
        {
            Users = result,
            CurrentPage = request.Page,
            Next = request.Page < pageCount,
            Previous = request.Page>1,
            PageCount = int.Parse(pageCount.ToString(CultureInfo.InvariantCulture))
        };
    }
}