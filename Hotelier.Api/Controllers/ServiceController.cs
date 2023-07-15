using Hotelier.Application.Services.Commands.CreateService;
using Hotelier.Application.Services.Commands.DeleteService;
using Hotelier.Application.Services.Commands.UpdateService;
using Hotelier.Application.Services.Queries.Dtos;
using Hotelier.Application.Services.Queries.GetService;
using Hotelier.Application.Services.Queries.GetServices;
using Microsoft.AspNetCore.Mvc;

namespace Hotelier.Api.Controllers;

public class ServiceController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDto>> Get(long id)
    {
        return Ok(await Mediator.Send(new GetServiceQuery{Id = id}));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ServiceDto>>> GetList([FromQuery] GetServicesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromForm] CreateServiceCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update([FromForm] UpdateServiceCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(long id)
    {
        await Mediator.Send(new DeleteServiceCommand() { Id = id });
        return NoContent();
    }
}