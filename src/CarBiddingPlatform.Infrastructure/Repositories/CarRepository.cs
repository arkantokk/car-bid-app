using CarBiddingPlatform.Application.Interfaces;
using CarBiddingPlatform.Domain.Entities;
using CarBiddingPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarBiddingPlatform.Infrastructure.Repositories;

public class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _context;

    public CarRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateCarAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
        return car.Id;
    }

    public async Task<Car?> FindCarByIdAsync(Guid id)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        return car ?? throw new KeyNotFoundException("No car with such id");
    }
}