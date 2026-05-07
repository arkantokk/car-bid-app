using MediatR;

namespace CarBiddingPlatform.Application.Commands.CreateAuction;

public record CreateAuctionCommand(
    Guid CarId,
    decimal StartingPrice,
    DateTime EndTime
    ) : IRequest<Guid>;