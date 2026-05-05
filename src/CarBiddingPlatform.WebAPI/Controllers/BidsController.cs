using CarBiddingPlatform.Application;
using CarBiddingPlatform.Application.Commands.PlaceBid;
using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BidsController : ControllerBase
{
    private readonly PlaceBidCommandHandler _commandHandler;

    public BidsController(PlaceBidCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceBid([FromBody] PlaceBidCommand command)
    {
        try
        {
            var bid = await _commandHandler.HandleAsync(command);
            return Ok(bid);
        }
        catch
        {
            return NotFound();
        }

       
    }
}