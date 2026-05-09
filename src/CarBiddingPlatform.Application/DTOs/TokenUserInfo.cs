namespace CarBiddingPlatform.Application.DTOs;

public record TokenUserInfo(
    string Email,
    string UserId
    // TODO: add roles
    );