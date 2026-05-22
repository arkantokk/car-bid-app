using MediatR;

namespace CarBiddingPlatform.Application.Commands.CreateCar;

public record CreateCarCommand(
    string Brand,
    string Model,
    int Year,
    decimal Price,
    Stream ImageStream,
    string ImageFileName,
    string UserId
    ) : IRequest<Guid>;