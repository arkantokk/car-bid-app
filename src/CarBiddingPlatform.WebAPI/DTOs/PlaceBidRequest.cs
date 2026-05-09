namespace CarBiddingPlatform.WebAPI.DTOs;

public record PlaceBidRequest(
    decimal Amount,
    Guid AuctionId
    );