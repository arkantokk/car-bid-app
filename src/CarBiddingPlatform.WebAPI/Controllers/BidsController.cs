using CarBiddingPlatform.Application;
using CarBiddingPlatform.Application.Commands.PlaceBid;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BidsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BidsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceBid([FromBody] PlaceBidCommand command)
    {
            var bid = await _mediator.Send(command);
            return Ok(bid);
    }
}