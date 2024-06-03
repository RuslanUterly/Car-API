using CarApi.Model;
using CarAPI.Dto;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarAPI.Mapping;

public static class CarMapping
{
    public static Car ToCar(this CarCreateDto carDto)
    {
        return new Car(
            default,
            carDto.Brand,
            carDto.Model,
            carDto.Power,
            carDto.Date
        );
    }

    public static Car ToCar(this CarUpdateDto carDto, int id)
    {
        return new Car(
            id,
            carDto.Brand,
            carDto.Model,
            carDto.Power,
            carDto.Date
        );
    }

    public static CarCreateDto ToCarDto(this Car car)
    {
        return new CarCreateDto(
            car.Brand,
            car.Model,
            car.Power,
            car.Date
        );
    }
}
