namespace CarBiddingPlatform.Application.DTOs;

public record BidDto(
    string BidOwner, 
    decimal Amount, 
    DateTime TimeStamp
);

public record AuctionDetailsDto(
    Guid Id,
    string CarBrand,
    string CarModel,
    string ImageUrl,
    decimal StartingPrice,
    decimal CurrentHighestBid,
    DateTime EndTime,
    DateTime StartTime,
    string SellerId,
    List<BidDto> Bids
);