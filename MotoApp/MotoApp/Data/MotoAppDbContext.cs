namespace MotoApp.Data;

using Microsoft.EntityFrameworkCore;
using MotoApp.Entities;

public class MotoAppDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Manager> Manager => Set<Manager>();

    public DbSet<Car> BusinessPartners => Set<Car>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("StorageAppDb");
    }
}

