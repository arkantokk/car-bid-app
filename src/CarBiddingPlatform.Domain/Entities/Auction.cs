namespace CarBiddingPlatform.Domain.Entities;

public class Auction
{
    public Guid Id { get; private set; }
    public Guid CarId { get; private set; }
    public decimal StartingPrice { get; private set; }
    public decimal CurrentHighestBid { get; private set; }
    public byte[] Version { get; private set; }
    private readonly List<Bid> _bids = [];
    public IReadOnlyCollection<Bid> Bids => _bids.AsReadOnly();
    public DateTime EndTime { get; private set; }

    public void PlaceBid(Bid newBid)
    {
        if (DateTime.UtcNow > EndTime) throw new Exception("Auction has ended.");
        if (newBid.Amount <= CurrentHighestBid) throw new Exception("Bid must be higher than current highest bid.");
        _bids.Add(newBid);
        CurrentHighestBid = newBid.Amount;
    }

    public Auction(Guid carId, decimal startingPrice, DateTime endTime)
    {
        if (DateTime.UtcNow > endTime) throw new Exception("Incorrect date");
        if (startingPrice < 0) throw new Exception("Starting price cant be less than 0");
        Id = Guid.NewGuid();
        CarId = carId;
        StartingPrice = startingPrice;
        CurrentHighestBid = startingPrice;
        EndTime = endTime;
    }
}