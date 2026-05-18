using CarBiddingPlatform.Application.DTOs;
using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Domain.Entities;
using CarBiddingPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarBiddingPlatform.Infrastructure.Repositories;

public class AuctionRepository : IAuctionRepository
{
    private readonly ApplicationDbContext _context;

    public AuctionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Auction?> GetAuctionByIdAsync(Guid id)
    {
        var auction = await _context.Auctions.Include(auctions => auctions.Bids).FirstOrDefaultAsync(a => a.Id == id);
        return auction;
    }

    public async Task<List<AuctionListDto>> GetAllAuctionsAsync()
    {
        var query = _context.Auctions
            .AsNoTracking()
            .Join(
                _context.Cars.AsNoTracking(),
                auction => auction.CarId,
                car => car.Id,
                (auction, car) => new AuctionListDto(
                    auction.Id,
                    car.Brand,
                    car.Model,
                    auction.CurrentHighestBid,
                    auction.EndTime
                )
            );

        var auctions = await query.ToListAsync();

// TODO: PAGINATION

        return auctions;
    }

    public async Task<bool> CreateAuctionAsync(Auction auction)
    {
        await _context.Auctions.AddAsync(auction);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SaveAuctionAsync(Auction auction)
    {
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<AuctionDetailsDto?> GetAuctionDetailsByIdAsync(Guid id)
    {
        var query = from auction in _context.Auctions.AsNoTracking().Include(a => a.Bids)
            join car in _context.Cars.AsNoTracking() on auction.CarId equals car.Id
            where auction.Id == id
            select new AuctionDetailsDto(
                auction.Id,
                car.Brand,
                car.Model,
                auction.StartingPrice,
                auction.CurrentHighestBid,
                auction.EndTime,
                auction.StartTime,
                auction.SellerId,
                auction.Bids.OrderByDescending(b => b.TimeStamp)
                    .Select(b => new BidDto(b.BidOwner, b.Amount, b.TimeStamp))
                    .ToList()
            );

        return await query.FirstOrDefaultAsync();
    }
}