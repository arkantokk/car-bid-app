using CarBiddingPlatform.Application.DTOs;
using MediatR;

namespace CarBiddingPlatform.Application.Commands.Auth.Register;

public record RegisterCommand(
    string Email, string Password, string UserName) : IRequest<AuthResponse>;