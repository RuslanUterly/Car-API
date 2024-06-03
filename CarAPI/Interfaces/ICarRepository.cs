using CarApi.Model;

namespace CarAPI.Interfaces;

public interface ICarRepository
{
    Task<ICollection<Car>> GetCars();
    Task<Car> GetCar(int id);
    Task<bool> CreateCar(Car car);
    Task<bool> UpdateCar(Car car);
    Task<bool> DeleteCar(Car car);

    Task<bool> CarExists(int carId);
    Task<bool> Save();
}
