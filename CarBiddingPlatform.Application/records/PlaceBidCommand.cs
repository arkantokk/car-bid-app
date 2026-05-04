namespace CarBiddingPlatform.Application;

public record PlaceBidCommand(
    string BidOwner,
    decimal Amount,
    Guid AuctionId
    );