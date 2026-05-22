using CarBiddingPlatform.Application.Interfaces;
using MediatR;

namespace CarBiddingPlatform.Application.Queries.GetWonAuctions;

public class GetWonAuctionsQueryHandler : IRequestHandler<GetWonAuctionsQuery, List<AuctionListDto>>
{
    private readonly IAuctionRepository _repository;

    public GetWonAuctionsQueryHandler(IAuctionRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<AuctionListDto>> Handle(GetWonAuctionsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetWonAuctionsAsync(request.UserId);
    }
}