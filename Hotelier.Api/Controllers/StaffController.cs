using Hotelier.Application.Staffs.Commands.CreateStaff;
using Hotelier.Application.Staffs.Commands.DeleteStaff;
using Hotelier.Application.Staffs.Commands.UpdateStaff;
using Hotelier.Application.Staffs.Queries.Dtos;
using Hotelier.Application.Staffs.Queries.GetStaff;
using Hotelier.Application.Staffs.Queries.GetStaffs;
using Microsoft.AspNetCore.Mvc;

namespace Hotelier.Api.Controllers;

public class StaffController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<StaffDto>> Get(long id)
    {
        return Ok(await Mediator.Send(new GetStaffQuery{Id = id}));
    }
    
    [HttpGet]
    public async Task<ActionResult<GetStaffsVm>> GetList([FromQuery] GetStaffsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromForm] CreateStaffCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update([FromForm] UpdateStaffCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(long id)
    {
        await Mediator.Send(new DeleteStaffCommand() { Id = id });
        return NoContent();
    }
}