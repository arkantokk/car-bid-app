using MediatR;

namespace CarBiddingPlatform.Application.Notifications;

public record BidPlacedNotification(
    Guid AuctionId, 
    string BidOwner,
    decimal Amount, 
    DateTime TimeStamp,
    DateTime EndTime
) : INotification;