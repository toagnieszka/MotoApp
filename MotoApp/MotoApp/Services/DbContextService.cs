using MotoApp.Components.CsvReader;
using MotoApp.Data;
using MotoApp.Data.Entities;
using System.Text.Json;

namespace MotoApp.Services
{
    public class DbContextService : IDbContextService
    {
        private readonly ICsvReader _csvReader;
        private readonly MotoAppDbContext _dbContext;

        public DbContextService
            (ICsvReader csvReader,
            MotoAppDbContext dbContext)
        {
            _csvReader = csvReader;
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public void InsertCarData()
        {
            var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");

            foreach (var car in cars)
            {
                _dbContext.Car.Add(new Car()
                {
                    Manufacturer = car.Manufacturer,
                    Name = car.Name,
                    Year = car.Year,
                    City = car.City,
                    Combined = car.Combined,
                    Cylinders = car.Cylinders,
                    Displacement = car.Displacement,
                    Highway = car.Highway,

                });
            }
            _dbContext.SaveChanges();
        }

        public void InsertBusinessPartnerData()
        {
            var businessPartners = _csvReader.ProcessManufacturers("Resources\\Files\\manufacturers.csv");

            foreach (var businessPartner in businessPartners)
            {
                _dbContext.BusinessPartner.Add(new BusinessPartner()
                {
                    Name = businessPartner.Name,
                    Country = businessPartner.Country,
                    Year = businessPartner.Year,
                });
            }
            _dbContext.SaveChanges();
        }

        public void InsertEmployeeData(string file)
        {
            if (File.Exists(file))
            {
                var serializedItems = File.ReadAllText(file);
                var deserializedItems = JsonSerializer.Deserialize<IEnumerable<Employee>>(serializedItems);
                if (deserializedItems != null)
                {
                    foreach (var employee in deserializedItems)
                    {
                        _dbContext.Employees.Add(new Employee()
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                        });
                    }
                }
            }
            _dbContext.SaveChanges();
        }

        public void ReadAllCarsFromDb()
        {
            var carsFromDb = _dbContext.Car.ToList();
            foreach (var car in carsFromDb)
            {
                Console.WriteLine($"{car.Name}: {car.Combined}, ID: {car.Id}");
            }
        }

        public void ReadAllBusinessPartnersFromDb()
        {
            var businessPartnersFromDb = _dbContext.BusinessPartner.ToList();
            foreach (var businessPartner in businessPartnersFromDb)
            {
                Console.WriteLine($"{businessPartner.Name}, ID: {businessPartner.Id}");
            }
        }

        public void ReadAllEmployeesFromDb()
        {
            var employeesFromDb = _dbContext.Employees.ToList();
            foreach (var employee in employeesFromDb)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName}, ID: {employee.Id}");
            }
        }

        public Car? FindCarById(int id)
        {
            return _dbContext.Car.FirstOrDefault(x => x.Id == id);
        }

        public BusinessPartner? FindBusinessPartnerById(int id)
        {

            return _dbContext.BusinessPartner.FirstOrDefault(x => x.Id == id);
        }

        public Employee? FindEmployeeById(int id)
        {
            return _dbContext.Employees.FirstOrDefault(x => x.Id == id);
        }

        public void RemoveCar()
        {
            ReadAllCarsFromDb();
            Console.WriteLine("\nWybierz który samochód chcesz usunąć wpisując jego ID:\n");
            int id = int.Parse(Console.ReadLine());
            var car = FindCarById(id);
            _dbContext.Car.Remove(car);
            _dbContext.SaveChanges();
        }

        public void RemoveBusinessPartner()
        {
            ReadAllBusinessPartnersFromDb();
            Console.WriteLine("\nWybierz którego biznes partnera chcesz usunąć wpisując jego ID:\n");
            int id = int.Parse(Console.ReadLine());
            var businessPartner = FindBusinessPartnerById(id);
            _dbContext.BusinessPartner.Remove(businessPartner);
            _dbContext.SaveChanges();
        }

        public void RemoveEmployee()
        {
            ReadAllEmployeesFromDb();
            Console.WriteLine("\nWybierz którego pracownika chcesz usunąć wpisując jego ID:\n");
            int id = int.Parse(Console.ReadLine());
            var employee = FindEmployeeById(id);
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }
        public void UpdateEmployeeData()
        {
            ReadAllEmployeesFromDb();
            Console.WriteLine("\nWybierz którego pracownika chcesz edytować wpisując jego ID:\n");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("\nJeżeli chcesz zedytować imię wpisz - 1, jeżeli nazwisko wpisz - 2:\n");
            int userChoice = int.Parse(Console.ReadLine());
            if (userChoice == 1)
            {
                Console.WriteLine("\nWpisz nowe imię:\n");
                var name = Console.ReadLine();
                var employee = FindEmployeeById(id);
                if (employee != null)
                {

                    employee.FirstName = name;
                    _dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("\nNie znaleziono pracownika o takim numerze ID\n");
                }
            }
            else if (userChoice == 2)
            {
                Console.WriteLine("\nWpisz nowe nazwisko:\n");
                var lastName = Console.ReadLine();
                var employee = FindEmployeeById(id);
                if (employee != null)
                {

                    employee.LastName = lastName;
                    _dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("\nNie znaleziono pracownika o takim numerze ID\n");
                }
            }
            else
            {
                Console.WriteLine("Zły numer");
            }
        }
        public void AddEmployeeToDb(string firstName, string lastName)
        {
            _dbContext.Employees.Add(new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
            });
            _dbContext.SaveChanges();
        }
    }
}
