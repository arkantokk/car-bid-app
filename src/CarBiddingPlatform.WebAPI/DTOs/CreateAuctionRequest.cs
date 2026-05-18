namespace CarBiddingPlatform.WebAPI.DTOs;

public record CreateAuctionRequest(
    Guid CarId,
    decimal StartingPrice,
    DateTime StartTime
    );