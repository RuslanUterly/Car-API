using System.ComponentModel.DataAnnotations;

namespace CarAPI.Dto;

public record class CarUpdateDto(
    [Required] string Brand,
    [Required] string Model,
    [Range(50, 1000)] int Power,
    DateOnly Date
);
