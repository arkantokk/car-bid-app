using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Domain.Entities;
using MediatR;

namespace CarBiddingPlatform.Application.Commands.CreateCar;

public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
{
    private readonly ICarRepository _repository;

    public CreateCarCommandHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateCarCommand command, CancellationToken cancellationToken)
    {
        var car = new Car(command.Brand, command.Model, command.Year, command.Price, command.UserId);
        return await _repository.CreateCarAsync(car);
    }
}