using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Domain.Entities;

namespace CarBiddingPlatform.Application.Commands.CreateCar;

public class CreateCarCommandHandler
{
    private readonly ICarRepository _repository;

    public CreateCarCommandHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> HandleAsync(CreateCarCommand command)
    {
        var car = new Car(command.Brand, command.Model, command.Year, command.Price, command.UserId);
        return await _repository.CreateCarAsync(car);
    }
}