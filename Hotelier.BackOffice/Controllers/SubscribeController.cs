using Microsoft.AspNetCore.Mvc;

namespace Hotelier.BackOffice.Controllers;

public class SubscribeController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}