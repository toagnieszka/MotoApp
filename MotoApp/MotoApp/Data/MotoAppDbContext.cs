namespace MotoApp.Data;

using Microsoft.EntityFrameworkCore;
using MotoApp.Data.Entities;

public class MotoAppDbContext : DbContext
{
    public MotoAppDbContext(DbContextOptions<MotoAppDbContext> options) 
        : base(options) 
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<BusinessPartner> BusinessPartner => Set<BusinessPartner>();

    public DbSet<Car> Car => Set<Car>();
}
