public record AuctionListDto(
    Guid Id, 
    string CarBrand, 
    string CarModel, 
    decimal CurrentHighestBid, 
    DateTime EndTime
);