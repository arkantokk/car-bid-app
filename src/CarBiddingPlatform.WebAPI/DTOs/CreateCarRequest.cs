namespace CarBiddingPlatform.WebAPI.DTOs;

public record CreateCarRequest(string Brand, string Model, int Year, decimal Price);