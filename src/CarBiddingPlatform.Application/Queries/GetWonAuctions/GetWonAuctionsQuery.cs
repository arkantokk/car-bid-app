using MediatR;

namespace CarBiddingPlatform.Application.Queries.GetWonAuctions;

public record GetWonAuctionsQuery(
    string UserId
    ) : IRequest<List<AuctionListDto>>;