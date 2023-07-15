using Hotelier.Application.Auth.Queries.Dtos;
using Hotelier.Application.Common.Managers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using User = Hotelier.Domain.IdentityEntities.User;

namespace Hotelier.Application.Auth.Queries.RefreshToken;

public class RefreshTokenCommand : IRequest<LoginDto>
{
    public string RefreshToken { get; set; }

    public class Handler : IRequestHandler<RefreshTokenCommand, LoginDto>
    {
        private readonly TokenManager _tokenManager;
        private readonly UserManager<User> _userManager;

        public Handler(TokenManager tokenManager, UserManager<User> userManager)
        {
            _tokenManager = tokenManager;
            _userManager = userManager;
        }

        public async Task<LoginDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            LoginDto loginViewModel = new LoginDto();
            User? appUser = _userManager.Users.FirstOrDefault(x => x.RefreshToken == request.RefreshToken);
            
            if (appUser != null)
            {
                loginViewModel = await _tokenManager.GenerateToken(appUser);
                return loginViewModel;
            }

            return loginViewModel;
        }
    }
}