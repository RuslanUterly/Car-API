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
    public async Task<IActionResult> GetCars()
    {
        var cars = await _carRepository.GetCars();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(cars.Select(c => c.ToCarDto()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetCar(int id)
    {
        if (!await _carRepository.CarExists(id))
            return NotFound();

        var category = await _carRepository.GetCar(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(category.ToCarDto());
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateCar([FromBody] CarCreateDto carCreate)
    {
        if (carCreate == null)
            return BadRequest();

        var car = await _carRepository.GetCars();

        if (car.Where(c => c.ToCarDto() == carCreate).FirstOrDefault() != null)
        {
            ModelState.AddModelError("", "Car already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _carRepository.CreateCar(carCreate.ToCar()))
        {
            ModelState.AddModelError("", "Smth went wrong");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfull");
    }
    
    [HttpPut("{carId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateCar(int carId, [FromBody] CarUpdateDto carUpdate)
    {
        if (carUpdate == null)
            return BadRequest();

        var car = await _carRepository.GetCars();

        if (car.Where(c => c.Id == carId).FirstOrDefault() == null)
        {
            ModelState.AddModelError("", "Car not created");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _carRepository.UpdateCar(carUpdate.ToCar(carId)))
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
    public async Task<IActionResult> DeleteCar(int carId)
    {
        if (!await _carRepository.CarExists(carId))
            return NotFound();

        var car = await _carRepository.GetCar(carId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _carRepository.DeleteCar(car))
        {
            ModelState.AddModelError("", "Smth went wrong");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfull");
    }
}
