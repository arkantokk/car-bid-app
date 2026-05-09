using MediatR;

namespace CarBiddingPlatform.Application.Notifications;

public record BidPlacedNotification(
    Guid AuctionId, 
    string BidOwner, 
    decimal Amount, 
    DateTime TimeStamp
) : INotification;