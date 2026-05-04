using CarBiddingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarBiddingPlatform.Infrastructure.Data;

public class BiddingDbContext : DbContext
{
    public BiddingDbContext(DbContextOptions<BiddingDbContext> dbContext) : base(dbContext)
    {
        
    }

    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Bid> Bids { get; set; }
}