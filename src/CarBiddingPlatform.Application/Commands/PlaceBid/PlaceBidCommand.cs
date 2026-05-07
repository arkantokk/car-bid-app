using MediatR;

namespace CarBiddingPlatform.Application.Commands.PlaceBid;

public record PlaceBidCommand(
    string BidOwner,
    decimal Amount,
    Guid AuctionId
    ) : IRequest<bool>;