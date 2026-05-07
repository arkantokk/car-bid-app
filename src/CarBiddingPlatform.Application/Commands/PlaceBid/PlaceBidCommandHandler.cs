using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Domain.Entities;
using MediatR;

namespace CarBiddingPlatform.Application.Commands.PlaceBid;

public class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, bool>
{
    private readonly IAuctionRepository _repository;

    public PlaceBidCommandHandler(IAuctionRepository repository)
    {
        _repository = repository;
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
        return await _repository.SaveAuctionAsync(auction);
    }
}