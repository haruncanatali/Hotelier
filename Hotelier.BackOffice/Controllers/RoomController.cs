using Microsoft.AspNetCore.Mvc;

namespace Hotelier.BackOffice.Controllers;

public class RoomController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}