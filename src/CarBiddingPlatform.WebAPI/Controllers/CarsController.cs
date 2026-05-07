using CarBiddingPlatform.Application.Commands.CreateCar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarsAsync([FromBody] CreateCarCommand command)
    {
        try
        {
            var car = await _mediator.Send(command);
            return Ok(car);
        }
        catch
        {
            return BadRequest();
        }
    }
}