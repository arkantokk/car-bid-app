using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Domain.Entities;

namespace CarBiddingPlatform.Application.commands;

public class PlaceBidCommandHandler
{
    private readonly IAuctionRepository _repository;

    public PlaceBidCommandHandler(IAuctionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Auction> HandleAsync(PlaceBidCommand bidCommand)
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