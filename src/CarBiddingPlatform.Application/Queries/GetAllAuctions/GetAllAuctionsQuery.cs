using CarBiddingPlatform.Application.DTOs;
using MediatR;

namespace CarBiddingPlatform.Application.Queries.GetAllAuctions;

public record GetAllAuctionsQuery() : IRequest<List<AuctionListDto>>;