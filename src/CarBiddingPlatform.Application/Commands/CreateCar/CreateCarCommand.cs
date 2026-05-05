namespace CarBiddingPlatform.Application.Commands.CreateCar;

public record CreateCarCommand(
    string Brand,
    string Model,
    int Year,
    decimal Price,
    string UserId
    );