namespace CarBiddingPlatform.Domain.Entities;

public class Bid
{
    public Guid Id { get; private set; }
    public string BidOwner { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime TimeStamp {get; private set; }

    public Bid(string bidOwner, decimal amount)
    {
        Id = Guid.NewGuid();
        BidOwner = bidOwner;
        Amount = amount;
        TimeStamp = DateTime.UtcNow;
    }
}