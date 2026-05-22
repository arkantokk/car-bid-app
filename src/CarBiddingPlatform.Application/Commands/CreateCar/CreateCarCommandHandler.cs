using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Domain.Entities;
using MediatR;

namespace CarBiddingPlatform.Application.Commands.CreateCar;

public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
{
    private readonly ICarRepository _repository;
    private readonly IBlobStorageService _blobStorageService;

    public CreateCarCommandHandler(ICarRepository repository, IBlobStorageService blobStorageService)
    {
        _repository = repository;
        _blobStorageService = blobStorageService;
    }

    public async Task<Guid> Handle(CreateCarCommand command, CancellationToken cancellationToken)
    {
        string? imageUrl = null;
        if (command.ImageStream != null && command.ImageStream.Length > 0)
        {
            imageUrl = await _blobStorageService.SaveImageAsync(command.ImageStream, command.ImageFileName);
        }
        
        var car = new Car(command.Brand, command.Model, command.Year, command.Price, imageUrl, command.UserId);
        return await _repository.CreateCarAsync(car);
    }
}