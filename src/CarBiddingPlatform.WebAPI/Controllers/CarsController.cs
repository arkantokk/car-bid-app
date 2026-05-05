using CarBiddingPlatform.Application.Commands.CreateCar;
using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly CreateCarCommandHandler _createCarCommandHandler;

    public CarsController(CreateCarCommandHandler createCarCommandHandler)
    {
        _createCarCommandHandler = createCarCommandHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarsAsync([FromBody] CreateCarCommand command)
    {
        try
        {
            var car = await _createCarCommandHandler.HandleAsync(command);
            return Ok(car);
        }
        catch
        {
            return BadRequest();
        }
    }
}