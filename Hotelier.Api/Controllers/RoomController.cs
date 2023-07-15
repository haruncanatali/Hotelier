using Hotelier.Application.Rooms.Commands.CreateRoom;
using Hotelier.Application.Rooms.Commands.DeleteRoom;
using Hotelier.Application.Rooms.Commands.UpdateRoom;
using Hotelier.Application.Rooms.Queries.Dtos;
using Hotelier.Application.Rooms.Queries.GetRoom;
using Hotelier.Application.Rooms.Queries.GetRooms;
using Microsoft.AspNetCore.Mvc;

namespace Hotelier.Api.Controllers;

public class RoomController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDto>> Get(long id)
    {
        return Ok(await Mediator.Send(new GetRoomQuery{Id = id}));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<RoomDto>>> GetList([FromQuery] GetRoomsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromForm] CreateRoomCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update([FromForm] UpdateRoomCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(long id)
    {
        await Mediator.Send(new DeleteRoomCommand() { Id = id });
        return NoContent();
    }
}