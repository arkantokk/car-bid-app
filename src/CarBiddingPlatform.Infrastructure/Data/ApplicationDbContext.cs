using CarBiddingPlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarBiddingPlatform.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
    {
        
    }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Bid> Bids { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tell SQL Server exactly how to format the decimals (18 total digits, 2 decimal places)
        modelBuilder.Entity<Car>().Property(c => c.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Bid>().Property(b => b.Amount).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Auction>().Property(a => a.StartingPrice).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Auction>().Property(a => a.CurrentHighestBid).HasColumnType("decimal(18,2)");

        base.OnModelCreating(modelBuilder);
    }
}