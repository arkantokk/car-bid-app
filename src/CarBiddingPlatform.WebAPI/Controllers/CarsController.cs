using System.Security.Claims;
using CarBiddingPlatform.Application.Commands.CreateCar;
using CarBiddingPlatform.WebAPI.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarsAsync([FromBody] CreateCarRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();
        var command = new CreateCarCommand(
            request.Brand, 
            request.Model, 
            request.Year, 
            request.Price, 
            userId
        );
        var carId = await _mediator.Send(command);
        return Created(string.Empty, new { CarId = carId });
    }
}