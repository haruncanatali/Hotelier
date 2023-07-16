using Hotelier.BackOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotelier.BackOffice.Controllers;

public class AuthController : Controller
{
    [AllowAnonymous]
    [HttpGet]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index()
    {
        return View(new LoginRequestModel());
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("AccessDenied")]
    public async Task<IActionResult> AccessDenied()
    {
        return View();
    }
}