using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Application.Notifications;
using CarBiddingPlatform.WebAPI.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CarBiddingPlatform.WebAPI.SignalRHandlers;

public class BidPlacedSignalRHandler : INotificationHandler<BidPlacedNotification>
{
    private readonly IHubContext<AuctionHub> _hubContext;
    
    public BidPlacedSignalRHandler(IHubContext<AuctionHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task Handle(BidPlacedNotification notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.Group(notification.AuctionId.ToString())
            .SendAsync("ReceiveNewBid", new 
            {
                notification.AuctionId,
                notification.BidOwner,
                notification.Amount,
                notification.TimeStamp
            }, cancellationToken);
    }
}