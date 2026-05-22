namespace CarBiddingPlatform.WebAPI.DTOs;

public record CreateCarRequest(string Brand, string Model, int Year, IFormFile? Image, decimal Price);