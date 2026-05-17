using CarBiddingPlatform.Application.DTOs;
using CarBiddingPlatform.Application.Interfaces;
using MediatR;

namespace CarBiddingPlatform.Application.Queries.GetAllAuctions;

public class GetAllAuctionsQueryHandler : IRequestHandler<GetAllAuctionsQuery, List<AuctionListDto>>
{
    private readonly IAuctionRepository _repository;

    public GetAllAuctionsQueryHandler(IAuctionRepository auctionRepository)
    {
        _repository = auctionRepository;
    }


    public async Task<List<AuctionListDto>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken)
    {
        var auctions = await _repository.GetAllAuctionsAsync();
        return auctions;
    }
}