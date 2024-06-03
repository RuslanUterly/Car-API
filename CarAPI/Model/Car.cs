using System.ComponentModel.DataAnnotations;

namespace CarApi.Model;

public record class Car(
    int Id,
    [Required]string Brand,
    [Required]string Model,
    [Range(50,1000)]int Power,
    DateOnly Date
);
