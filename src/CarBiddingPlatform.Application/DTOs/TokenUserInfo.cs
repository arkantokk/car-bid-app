namespace CarBiddingPlatform.Application.DTOs;

public record TokenUserInfo(
    string Email,
    string UserId,
    string UserName
    // TODO: add roles
    );