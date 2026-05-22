using Microsoft.AspNetCore.Mvc;

namespace CarBiddingPlatform.WebAPI.DTOs;

public class CreateCarRequest
{
    [FromForm(Name = "brand")]
    public string Brand { get; set; } = string.Empty;

    [FromForm(Name = "model")]
    public string Model { get; set; } = string.Empty;

    [FromForm(Name = "year")]
    public int Year { get; set; }

    [FromForm(Name = "price")]
    public decimal Price { get; set; }

    public IFormFile? Image { get; set; }
}