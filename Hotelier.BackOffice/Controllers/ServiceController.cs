using Microsoft.AspNetCore.Mvc;

namespace Hotelier.BackOffice.Controllers;

public class ServiceController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}