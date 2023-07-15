using Hotelier.Application.Testimonials.Commands.CreateTestimonial;
using Hotelier.Application.Testimonials.Commands.DeleteTestimonial;
using Hotelier.Application.Testimonials.Commands.UpdateTestimonial;
using Hotelier.Application.Testimonials.Queries.Dtos;
using Hotelier.Application.Testimonials.Queries.GetTestimonial;
using Hotelier.Application.Testimonials.Queries.GetTestimonials;
using Microsoft.AspNetCore.Mvc;

namespace Hotelier.Api.Controllers;

public class TestimonialController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<TestimonialDto>> Get(long id)
    {
        return Ok(await Mediator.Send(new GetTestimonialQuery{Id = id}));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TestimonialDto>>> GetList([FromQuery] GetTestimonialsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromForm] CreateTestimonialCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update([FromForm] UpdateTestimonialCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(long id)
    {
        await Mediator.Send(new DeleteTestimonialCommand() { Id = id });
        return NoContent();
    }
}