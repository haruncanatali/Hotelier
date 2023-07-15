using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Users.Queries.Dtos;
using Hotelier.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hotelier.Application.Auth.Queries.PasswordChange;

public class PasswordChangeCommand : IRequest<UserDto>
{
    public string Password { get; set; }

    public class Handler : IRequestHandler<PasswordChangeCommand, UserDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public Handler(UserManager<User> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<UserDto> Handle(PasswordChangeCommand request, CancellationToken cancellationToken)
        {
            UserDto result = null;
            
            User? appUser = _userManager.Users.FirstOrDefault(x => x.Id == _currentUserService.UserId);
            
            if (appUser != null)
            {
                var removeResult = await _userManager.RemovePasswordAsync(appUser);
                if (removeResult.Succeeded)
                {
                    var addResult = await _userManager.AddPasswordAsync(appUser, request.Password);

                    if (addResult.Succeeded)
                    {
                        result = new UserDto
                        {
                            Birthdate = appUser.Birthdate,
                            FirstName = appUser.FirstName,
                            LastName = appUser.LastName,
                            ProfilePhoto = appUser.ProfilePhoto,
                            Email = appUser.Email
                        };
                    }
                }
            }

            return result;
        }
    }
}