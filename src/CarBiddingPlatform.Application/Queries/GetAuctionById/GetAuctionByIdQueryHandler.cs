using CarBiddingPlatform.Application.DTOs;
using CarBiddingPlatform.Application.Interfaces;
using MediatR;

namespace CarBiddingPlatform.Application.Queries.GetAuctionById;

public class GetAuctionByIdQueryHandler : IRequestHandler<GetAuctionByIdQuery, AuctionDetailsDto?>
{
    private readonly IAuctionRepository _repository;

    public GetAuctionByIdQueryHandler(IAuctionRepository repository)
    {
        _repository = repository;
    }

    public async Task<AuctionDetailsDto?> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAuctionDetailsByIdAsync(request.AuctionId);
    }
}