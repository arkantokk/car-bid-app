using System.Security.Claims;
using CarBiddingPlatform.Application.Commands.CreateAuction;
using CarBiddingPlatform.Application.Queries.GetAllAuctions;
using CarBiddingPlatform.Application.Queries.GetAuctionById;
using CarBiddingPlatform.Application.Queries.GetWonAuctions;
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
        var command = new CreateAuctionCommand(request.CarId, request.StartingPrice, request.StartTime, userId);
        var auctionId = await _mediator.Send(command);
        return Created(string.Empty, new { AuctionId = auctionId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuctionsAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Forbid();
        var query = new GetAllAuctionsQuery();
        var auctions = await _mediator.Send(query);
        return Ok(auctions);
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous] 
    public async Task<IActionResult> GetAuctionById(Guid id)
    {
        var query = new GetAuctionByIdQuery(id);
        var auction = await _mediator.Send(query);
        if (auction == null) return NotFound();
        return Ok(auction);
    }

    [HttpGet("won")]
    [Authorize]
    public async Task<IActionResult> GetWonAuctions()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Forbid();
        }
        var query = new GetWonAuctionsQuery(userId);
        var auctions = await _mediator.Send(query);
        return Ok(auctions);
    }
}