using MediatR;

namespace CarBiddingPlatform.Application.Queries.GetAuctionsByUserId;

public record GetAuctionsByUserIdQuery(
    string UserId
    ) : IRequest<List<AuctionListDto>>
    ;