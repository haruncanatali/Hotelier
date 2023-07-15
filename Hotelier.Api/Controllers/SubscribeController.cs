using Hotelier.Application.Subscribes.Commands.CreateSubscribe;
using Hotelier.Application.Subscribes.Commands.DeleteSubscribe;
using Hotelier.Application.Subscribes.Commands.UpdateSubscribe;
using Hotelier.Application.Subscribes.Queries.Dtos;
using Hotelier.Application.Subscribes.Queries.GetSubscribe;
using Hotelier.Application.Subscribes.Queries.GetSubscribes;
using Microsoft.AspNetCore.Mvc;

namespace Hotelier.Api.Controllers;

public class SubscribeController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<SubscribeDto>> Get(long id)
    {
        return Ok(await Mediator.Send(new GetSubscribeQuery{Id = id}));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<SubscribeDto>>> GetList([FromQuery] GetSubscribesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromForm] CreateSubscribeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update([FromForm] UpdateSubscribeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(long id)
    {
        await Mediator.Send(new DeleteSubscribeCommand() { Id = id });
        return NoContent();
    }
}