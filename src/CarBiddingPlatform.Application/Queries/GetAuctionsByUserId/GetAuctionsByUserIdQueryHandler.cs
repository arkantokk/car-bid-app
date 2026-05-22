using CarBiddingPlatform.Application.Interfaces;
using MediatR;

namespace CarBiddingPlatform.Application.Queries.GetAuctionsByUserId;

public class GetAuctionsByUserIdQueryHandler : IRequestHandler<GetAuctionsByUserIdQuery, List<AuctionListDto>>
{
    private readonly IAuctionRepository _repository;

    public GetAuctionsByUserIdQueryHandler(IAuctionRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<AuctionListDto>> Handle(GetAuctionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllUserAuctionsAsync(request.UserId);
    }
}