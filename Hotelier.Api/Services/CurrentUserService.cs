using System.Security.Claims;
using Hotelier.Application.Common.Interfaces;

namespace Hotelier.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        string UserIdStr = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        UserId = Convert.ToInt64(UserIdStr);
        IsAuthenticated = UserId != null;
    }

    public long UserId { get; } = 1;
    public bool IsAuthenticated { get; } = false;
}