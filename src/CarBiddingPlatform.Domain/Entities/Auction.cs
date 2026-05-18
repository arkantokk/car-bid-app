namespace CarBiddingPlatform.Domain.Entities;

public class Auction
{
    public Guid Id { get; private set; }
    public Guid CarId { get; private set; }
    public string SellerId { get; private set; }
    public decimal StartingPrice { get; private set; }
    public decimal CurrentHighestBid { get; private set; }
    public byte[] Version { get; private set; }
    private readonly List<Bid> _bids = [];
    public IReadOnlyCollection<Bid> Bids => _bids.AsReadOnly();
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    private Auction() { }

    public Auction(Guid carId, decimal startingPrice, DateTime startTime, string sellerId)
    {
        if (startingPrice < 0) throw new Exception("Starting price cant be less than 0");
        Id = Guid.NewGuid();
        CarId = carId;
        SellerId = sellerId;
        StartingPrice = startingPrice;
        CurrentHighestBid = startingPrice;
        StartTime = startTime;
        EndTime = startTime.AddMinutes(5);
    }

    public void PlaceBid(Bid newBid)
    {
        if (DateTime.UtcNow > EndTime) throw new Exception("Auction has ended.");
        if (newBid.Amount <= CurrentHighestBid) throw new Exception("Bid must be higher than current highest bid.");
        if (newBid.BidOwner == SellerId) throw new Exception("You cannot bid on your own auction.");
        if (newBid.TimeStamp < StartTime) throw new Exception("Auction didn't start yet");
        _bids.Add(newBid);
        
        EndTime = DateTime.UtcNow.AddSeconds(15);
        CurrentHighestBid = newBid.Amount;
    }
}