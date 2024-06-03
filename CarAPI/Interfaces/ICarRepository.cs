using CarApi.Model;

namespace CarAPI.Interfaces;

public interface ICarRepository
{
    ICollection<Car> GetCars();
    Car GetCar(int id);
    bool CreateCar(Car car);
    bool UpdateCar(Car car);
    bool DeleteCar(Car car);

    bool CarExists(int carId);
    bool Save();
}
