namespace CarBiddingPlatform.Application.DTOs;

public record AuthResponse(
    string JwtToken,
    string[] Errors
    );