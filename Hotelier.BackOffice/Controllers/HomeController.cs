using System.Diagnostics;
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