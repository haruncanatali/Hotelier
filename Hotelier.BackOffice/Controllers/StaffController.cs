using Microsoft.AspNetCore.Mvc;

namespace Hotelier.BackOffice.Controllers;

public class StaffController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}