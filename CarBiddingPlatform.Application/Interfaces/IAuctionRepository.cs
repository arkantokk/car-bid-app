using CarBiddingPlatform.Domain.Entities;

namespace CarBiddingPlatform.Application.Interfaces;

public interface IAuctionRepository
{
    Task<Auction?> GetAuctionByIdAsync(Guid id);
    Task<bool> SaveAuctionAsync(Auction auction);
}