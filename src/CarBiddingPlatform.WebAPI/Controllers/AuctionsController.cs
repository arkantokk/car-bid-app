using System.Security.Claims;
using CarBiddingPlatform.Application.Commands.CreateAuction;
using CarBiddingPlatform.WebAPI.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AuctionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuctionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuctionAsync([FromBody] CreateAuctionRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Forbid();
        var command = new CreateAuctionCommand(request.CarId, request.StartingPrice, request.EndTime, userId);
        var auctionId = await _mediator.Send(command);
        return Created(string.Empty, new { AuctionId = auctionId });
    }
}