using CarApi.Model;
using CarAPI.Data;
using CarAPI.Interfaces;

namespace CarAPI.Repository;

public class CarRepository(DataContext context) : ICarRepository
{
    private readonly DataContext _context = context;

    public bool CarExists(int carId)
    {
        return _context.Cars.Any(c => c.Id == carId);
    }

    public bool CreateCar(Car car)
    {
        _context.Add(car);
        return Save();
    }

    public bool DeleteCar(Car car)
    {
        _context.Remove(car);
        return Save();
    }

    public Car GetCar(int id)
    {
        return _context.Cars.FirstOrDefault(c => c.Id == id)!;
    }

    public ICollection<Car> GetCars()
    {
        return _context.Cars.ToArray();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool UpdateCar(Car car)
    {
        _context.Update(car);
        return Save();
    }
}
