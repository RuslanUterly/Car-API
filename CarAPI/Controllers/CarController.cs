using Microsoft.AspNetCore.Mvc;
using CarApi.Model;
using CarAPI.Interfaces;
using CarAPI.Mapping;
using CarAPI.Dto;

namespace CarApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController(ICarRepository carRepository) : ControllerBase
{
    private readonly ICarRepository _carRepository = carRepository;

    [HttpGet]
    public IActionResult GetCars()
    {
        var cars = _carRepository.GetCars().Select(c => c.ToCarDto());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(cars);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonByCategoryId(int id)
    {
        if (!_carRepository.CarExists(id))
            return NotFound();

        var category = _carRepository.GetCar(id).ToCarDto();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateCar([FromBody] CarCreateDto carCreate)
    {
        if (carCreate == null)
            return BadRequest();

        var car = _carRepository.GetCars().Where(c => c.ToCarDto() == carCreate).FirstOrDefault();

        if (car != null)
        {
            ModelState.AddModelError("", "Car already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_carRepository.CreateCar(carCreate.ToCar()))
        {
            ModelState.AddModelError("", "Smth went wrong");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfull");
    }

    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult DeleteCar(int carId)
    {
        if (!_carRepository.CarExists(carId))
            return NotFound();

        var car = _carRepository.GetCar(carId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_carRepository.DeleteCar(car))
        {
            ModelState.AddModelError("", "Smth went wrong");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfull");
    }
}
