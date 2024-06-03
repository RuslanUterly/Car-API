using CarApi.Model;
using CarAPI.Data;
using CarAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarAPI.Repository;

public class CarRepository(DataContext context) : ICarRepository
{
    private readonly DataContext _context = context;

    public async Task<bool> CarExists(int carId)
    {
        return await _context.Cars
            .AsNoTracking()
            .AnyAsync(c => c.Id == carId);
    }

    public async Task<bool> CreateCar(Car car)
    {
        await _context.Cars.AddAsync(car);
        return await Save();
    }

    public async Task<bool> DeleteCar(Car car)
    {
        await _context.Cars.Where(c => c == car).ExecuteDeleteAsync();
        return true;
    }

    public async Task<Car> GetCar(int id)
    {
        return await _context.Cars
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ICollection<Car>> GetCars()
    {
        return await _context.Cars.ToArrayAsync();
    }

    public async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0 ? true : false;
    }

    public async Task<bool> UpdateCar(Car car)
    {
        await _context.Cars.ExecuteUpdateAsync(c => c.SetProperty(c => c, car));
        return true;
    }
}
