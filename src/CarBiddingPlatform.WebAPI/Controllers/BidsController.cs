using System.Security.Claims;
using CarBiddingPlatform.Application.Commands.PlaceBid;
using CarBiddingPlatform.WebAPI.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BidsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BidsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceBid([FromBody] PlaceBidRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Forbid();
        var command = new PlaceBidCommand(
            userId, request.Amount, request.AuctionId
        );
        var bid = await _mediator.Send(command);
        return Ok(bid);
    }
}