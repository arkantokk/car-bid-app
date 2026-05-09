using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Application.Notifications;
using CarBiddingPlatform.Domain.Entities;
using MediatR;

namespace CarBiddingPlatform.Application.Commands.PlaceBid;

public class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, bool>
{
    private readonly IAuctionRepository _repository;
    private readonly IPublisher _publisher;
    public PlaceBidCommandHandler(IAuctionRepository repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<bool> Handle(PlaceBidCommand bidCommand, CancellationToken cancellationToken)
    {
        var auction = await _repository.GetAuctionByIdAsync(bidCommand.AuctionId);
        if (auction == null)
        {
            throw new KeyNotFoundException("Auction doesnt exist");
        }

        var bid = new Bid(bidCommand.BidOwner, bidCommand.Amount);
        auction.PlaceBid(bid);
        var result = await _repository.SaveAuctionAsync(auction);
        //Broadcasting the notification one to many connection
        if (result)
        {
            await _publisher.Publish(new BidPlacedNotification(
                auction.Id, bid.BidOwner, bid.Amount, bid.TimeStamp
            ), cancellationToken);
        }

        return result;
    }
}