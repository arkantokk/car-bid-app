using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Domain.Entities;
using CarBiddingPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarBiddingPlatform.Infrastructure.Repositories;

public class AuctionRepository : IAuctionRepository
{
    private readonly BiddingDbContext _context;

    public AuctionRepository(BiddingDbContext context)
    {
        _context = context;
    }

    public async Task<Auction?> GetAuctionByIdAsync(Guid id)
    {
        var auction = await _context.Auctions.Include(auctions => auctions.Bids).FirstOrDefaultAsync(a => a.Id == id);
        return auction;
    }

    public async Task<bool> SaveAuctionAsync(Auction auction){
        await _context.SaveChangesAsync();
        return true;
    }
}