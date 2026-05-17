namespace CarBiddingPlatform.Domain.Entities;

public class Bid
{
    public Guid Id { get; private set; }
    public string BidOwner { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime TimeStamp {get; private set; }

    public Bid(string bidOwner, decimal amount)
    {
        if (string.IsNullOrEmpty(bidOwner)) throw new Exception("Owner shouldn't be empty");
        if (amount < 0) throw new Exception("amount cant be empty");
        BidOwner = bidOwner;
        Amount = amount;
        TimeStamp = DateTime.UtcNow;
    }
}