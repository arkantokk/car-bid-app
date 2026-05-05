using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Domain.Entities;

namespace CarBiddingPlatform.Application.Commands.CreateAuction;

public class CreateAuctionCommandHandler
{
    private readonly IAuctionRepository _repository;

    public CreateAuctionCommandHandler(IAuctionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> HandleAsync(CreateAuctionCommand command)
    {
        var auction = new Auction(command.CarId, command.StartingPrice, command.EndTime);
        await _repository.CreateAuctionAsync(auction);
        return auction.Id;
    }
}