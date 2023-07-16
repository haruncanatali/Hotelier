using Microsoft.AspNetCore.Mvc;

namespace Hotelier.BackOffice.Controllers;

public class TestimonialController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}