using Hotelier.Application.Common.Interfaces;
using Hotelier.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hotelier.Application.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<bool>
{
    public long Id { get; set; }

    public class Handler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;

        public Handler(ICurrentUserService currentUserService, UserManager<User> userManager)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User? user = await _userManager.FindByIdAsync(request.Id.ToString());
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}