public record AuctionListDto(
    Guid Id, 
    string CarBrand, 
    string CarModel, 
    string ImageUrl,
    decimal CurrentHighestBid, 
    DateTime EndTime
);