using CarBiddingPlatform.Application.Commands.Auth.Login;
using CarBiddingPlatform.Application.Commands.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator ;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Errors.Length != 0) return BadRequest(result.Errors);
        return Ok(new { Token = result.JwtToken });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Errors.Length != 0) return BadRequest(result.Errors);
        return Ok(new { Token = result.JwtToken });
    }
}