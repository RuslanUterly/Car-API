using CarApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CarAPI.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Car> Cars => Set<Car>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().HasKey(c => c.Id);
        modelBuilder.Entity<Car>().HasData( 
                new {Id = 1, Brand = "BMW", Model =  "X5", Power = 200, Date = new DateOnly(2006, 03, 20) },
                new {Id = 2, Brand = "Mercedes", Model = "W140", Power =  180, Date = new DateOnly(1990, 03, 20) }
            );
    }
}
