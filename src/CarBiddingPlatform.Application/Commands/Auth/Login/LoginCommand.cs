using CarBiddingPlatform.Application.DTOs;
using MediatR;

namespace CarBiddingPlatform.Application.Commands.Auth.Login;

public record LoginCommand(
    string Email,
    string Password
    ) : IRequest<AuthResponse>;