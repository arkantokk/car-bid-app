using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Domain.Entities;
using MediatR;

namespace CarBiddingPlatform.Application.Commands.CreateAuction;

public class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, Guid>
{
    private readonly IAuctionRepository _repository;

    public CreateAuctionCommandHandler(IAuctionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateAuctionCommand command, CancellationToken cancellationToken)
    {
        var auction = new Auction(command.CarId, command.StartingPrice, command.StartTime, command.SellerId);
        await _repository.CreateAuctionAsync(auction);
        return auction.Id;
    }
}