namespace CarBiddingPlatform.Domain.Entities;

public class Car
{
    public Guid Id { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; } // change to enum
    public int Year { get; private set; }
    public decimal Price { get; private set; }
    public string UserId { get; private set; }
    public Car(string brand, string model, int year, decimal price, string userId)
    {
        if (string.IsNullOrEmpty(brand) ||
            string.IsNullOrEmpty(model) ||
            string.IsNullOrEmpty(userId)
           )
        {
            throw new Exception("Can't create empty car");
        }
        Id = Guid.NewGuid();
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
        UserId = userId;
    }
    
}