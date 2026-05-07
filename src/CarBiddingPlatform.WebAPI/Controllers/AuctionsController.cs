using CarBiddingPlatform.Application.Commands.CreateAuction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class AuctionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuctionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuctionAsync([FromBody] CreateAuctionCommand command)
    {
        try
        {
            var auction = await _mediator.Send(command); 
            return Ok(auction);
        }
        catch
        {
            return BadRequest();
        }
    }
}