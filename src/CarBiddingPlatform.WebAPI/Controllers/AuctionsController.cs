using CarBiddingPlatform.Application.Commands.CreateAuction;
using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class AuctionsController : ControllerBase
{
    private readonly CreateAuctionCommandHandler _createAuctionCommandHandler;

    public AuctionsController(CreateAuctionCommandHandler createAuctionCommandHandler)
    {
        _createAuctionCommandHandler = createAuctionCommandHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuctionAsync([FromBody] CreateAuctionCommand command)
    {
        try
        {
            var auction = await _createAuctionCommandHandler.HandleAsync(command);
            return Ok(auction);
        }
        catch
        {
            return BadRequest();
        }
    }
}