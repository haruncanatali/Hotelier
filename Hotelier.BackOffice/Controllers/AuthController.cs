using System.Security.Claims;
using Hotelier.Application.Auth.Queries.Dtos;
using Hotelier.BackOffice.Models;
using Hotelier.BackOffice.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotelier.BackOffice.Controllers;

[Authorize]
public class AuthController : Controller
{
    private readonly RequestHelper _requestHelper;

    public AuthController(RequestHelper requestHelper)
    {
        _requestHelper = requestHelper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(new LoginRequestModel());
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Index(LoginRequestModel? model)
    {
        if (ModelState.IsValid)
        {
            LoginDto? loginResult = await _requestHelper.SendLoginRequestAsync(model!);

            if (loginResult != null)
            {
                AuthProperties authResult = await _requestHelper.LoginCookieOperationsAsync(loginResult);
                
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(authResult.ClaimsIdentity),
                    authResult.AuthenticationProperties);

                return RedirectToAction("Index", "Home");
            }
        }
        else
        {
            return View(model);
        }
        return Redirect("/Auth/Index");
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("AccessDenied")]
    public async Task<IActionResult> AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Auth");
    }
}