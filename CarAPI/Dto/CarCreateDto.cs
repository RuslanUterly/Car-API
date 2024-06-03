using System.ComponentModel.DataAnnotations;

namespace CarAPI.Dto;

public record class CarCreateDto(
    [Required] string Brand,
    [Required] string Model,
    [Range(50, 1000)] int Power,
    DateOnly Date
);
