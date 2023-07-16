using Microsoft.AspNetCore.Mvc;

namespace Hotelier.BackOffice.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}