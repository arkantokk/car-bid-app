namespace CarBiddingPlatform.Application.DTOs;

public record IdentityResult(
    bool IsSuccess,
    string? UserId,
    string? Email,
    string[] Errors
    );