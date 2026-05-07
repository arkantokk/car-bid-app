using CarBiddingPlatform.Domain.Entities;

namespace CarBiddingPlatform.Application.Interfaces;

public interface ICarRepository
{
    Task<Guid> CreateCarAsync(Car car);
    Task<Car?> FindCarByIdAsync(Guid id);
    
}