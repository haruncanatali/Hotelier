using Hotelier.Application.Auth.Queries.Dtos;
using Hotelier.Application.Auth.Queries.Login;
using Hotelier.Application.Users.Commands.CreateUser;
using Hotelier.Application.Users.Commands.DeleteUser;
using Hotelier.Application.Users.Commands.UpdateUser;
using Hotelier.Application.Users.Queries.Dtos;
using Hotelier.Application.Users.Queries.GetUser;
using Hotelier.Application.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotelier.Api.Controllers;

public class UserController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<UserDto>> Get()
    {
        return Ok(await Mediator.Send(new GetUserQuery()));
    }
    
    [HttpGet]
    [Route("List")]
    public async Task<ActionResult<UserDto>> GetList([FromQuery]GetUsersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<LoginDto>> Create([FromForm] CreateUserCommand command)
    {
        var createResult = await Mediator.Send(command);

        if (createResult != null)
        {
            return Ok(await Mediator.Send(new LoginCommand
            {
                Email = command.Email,
                Password = command.Password
            }));
        }
        
        return BadRequest();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UserDto>> Update([FromForm] UpdateUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(long id)
    {
        await Mediator.Send(new DeleteUserCommand { Id = id });
        return NoContent();
    }
}