namespace CarBiddingPlatform.Domain.Entities;

public class Car
{
    public Guid Id { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public string Year { get; private set; }
    public decimal Price { get; private set; }
    public string OwnerId { get; private set; }
    public Car(string brand, string model, string year, decimal price, string ownerId)
    {
        Id = Guid.NewGuid();
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
        OwnerId = ownerId;
    }
    
}