using Hotelier.Application.Auth.Queries.Dtos;
using Hotelier.Application.Common.Managers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using User = Hotelier.Domain.IdentityEntities.User;

namespace Hotelier.Application.Auth.Queries.Login;

public class LoginCommand : IRequest<LoginDto>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public class Handler : IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly TokenManager _tokenManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public Handler(TokenManager tokenManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _tokenManager = tokenManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginDto loginViewModel = new LoginDto();
            User? appUser = await _userManager.FindByEmailAsync(request.Email);
            if (appUser != null)
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName, request.Password,
                    false, false);

                if (result.Succeeded)
                {
                    loginViewModel = await _tokenManager.GenerateToken(appUser);
                    appUser.RefreshToken = loginViewModel.RefreshToken;
                    await _userManager.UpdateAsync(appUser);
                }
            }

            return loginViewModel;
        }
    }
}