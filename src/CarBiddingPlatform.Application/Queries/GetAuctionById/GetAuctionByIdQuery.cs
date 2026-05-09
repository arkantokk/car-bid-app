using CarBiddingPlatform.Application.DTOs;
using MediatR;

namespace CarBiddingPlatform.Application.Queries.GetAuctionById;

public record GetAuctionByIdQuery(Guid AuctionId) : IRequest<AuctionDetailsDto?>;