using CarBiddingPlatform.Application.DTOs;
using CarBiddingPlatform.Domain.Entities;

namespace CarBiddingPlatform.Application.Interfaces;

public interface IAuctionRepository
{
    Task<Auction?> GetAuctionByIdAsync(Guid id);
    Task<List<AuctionListDto>> GetAllAuctionsAsync();
    Task<bool> CreateAuctionAsync(Auction auction);
    Task<bool> SaveAuctionAsync(Auction auction);
    Task<List<AuctionListDto>> GetWonAuctionsAsync(string userId);
    Task<AuctionDetailsDto?> GetAuctionDetailsByIdAsync(Guid id);
}