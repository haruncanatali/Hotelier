using Hotelier.Application.Auth.Queries.Dtos;
using Hotelier.Application.Auth.Queries.Login;
using Hotelier.Application.Auth.Queries.PasswordChange;
using Hotelier.Application.Auth.Queries.RefreshToken;
using Hotelier.Application.Users.Queries.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = Hotelier.Domain.IdentityEntities.User;

namespace Hotelier.Api.Controllers;

public class AuthController : BaseController
{
    private readonly SignInManager<User> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuthController(SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
    {
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<LoginDto>> Login(LoginCommand loginModel)
    {
        LoginDto loginResponse = await Mediator.Send(loginModel);
        return Ok(loginResponse);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("RefreshToken")]
    public async Task<ActionResult<LoginDto>> RefreshToken(string refreshToken)
    {
        return Ok(await Mediator.Send(new RefreshTokenCommand { RefreshToken = refreshToken }));
    }

    [HttpPost]
    [Route("PasswordChange")]
    public async Task<ActionResult<UserDto>> PasswordChange(PasswordChangeCommand request)
    {
        UserDto? result  = await Mediator.Send(request);
        
        LoginDto loginResponse = new();
        
        if (result != null)
        {
            loginResponse = await Mediator.Send(new LoginCommand
            {
                Password = request.Password,
                Email = result.Email
            });
        }

        return Ok(loginResponse);
    }

    [HttpGet]
    [Route("SignOut")]
    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        _httpContextAccessor.HttpContext.Response.Headers["Authorization"] = "";

        return Ok();
    }
}