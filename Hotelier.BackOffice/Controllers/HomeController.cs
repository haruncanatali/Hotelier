using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Hotelier.BackOffice.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hotelier.BackOffice.Controllers;

[Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

[Authorize]
public class WelcomeResponseViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WelcomeResponseViewComponent(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public IViewComponentResult Invoke()
    {
        WelcomeResponseModel model = new WelcomeResponseModel
        {
            FullName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name),
            WelcomeMessage = SetTime()
        };
        
        return View("_WelcomeResponse",model);
    }

    private string SetTime()
    {
        if (DateTime.Now.Hour < 10 && DateTime.Now.Hour > 6)
        {
            return "Günaydın,";
        }
        else if (DateTime.Now.Hour <= 6)
        {
            return "İyi geceler";
        }
        else if (DateTime.Now.Hour >= 10 && DateTime.Now.Hour < 18)
        {
            return "İyi günler,";
        }
        else
        {
            return "İyi akşamlar,";
        }
    }
}

[Authorize]
public class ProfileResponseViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProfileResponseViewComponent(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IViewComponentResult Invoke()
    {
        ProfileResponseModel model = new ProfileResponseModel
        {
            Id = long.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
            Mail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email),
            FirstName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name),
            LastName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Surname),
            Photo = _httpContextAccessor.HttpContext.User.FindFirstValue("Photo")
        };
        
        return View("_ProfileResponse",model);
    }
}

[Authorize]
public class PaginationResponseViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PaginationResponseModel model)
    {
        return View("_PaginationResponse",model);
    }
}