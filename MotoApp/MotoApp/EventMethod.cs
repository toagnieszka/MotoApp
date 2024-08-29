using MotoApp.DataProviders;
using MotoApp;
using System.Text.Json;
using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;

namespace MotoApp
{
    public class EventMethod : IEventMethod
    {
        const string auditFile = "AuditFile.txt";

        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Manager> _managerRepository;
        private readonly IRepository<Car> _carRepository;



        public EventMethod(
            IRepository<Employee> employeeRepository,
            IRepository<Manager> managerRepository,
            IRepository<Car> carRepository)
        {
            _employeeRepository = employeeRepository;
            _managerRepository = managerRepository;
            _carRepository = carRepository;
        }


        public void UseEvents()
        {
            _employeeRepository.ItemAdded += EmployeeAdded;
            _managerRepository.ItemAdded += ManagerAdded;
            _carRepository.ItemAdded += CarAdded;
            _employeeRepository.ItemRemoved += EmployeeRemoved;
            _managerRepository.ItemRemoved += ManagerRemoved;
            _carRepository.ItemRemoved += CarRemoved;
        }


        private static void EmployeeAdded(object? sender, Employee item)
        {
            Console.WriteLine($"{item.FirstName} {item.LastName} added");
            using (var writer = File.AppendText(auditFile))
            {
                writer.WriteLine($"[{DateTime.Now}]-EmployeeAdded-[{item.FirstName} {item.LastName}]");
            }
        }
        private static void ManagerAdded(object? sender, Manager item)
        {
            Console.WriteLine($"{item.FirstName} {item.LastName} added");
            using (var writer = File.AppendText(auditFile))
            {
                writer.WriteLine($"[{DateTime.Now}]-ManagerAdded-[{item.FirstName} {item.LastName}]");
            }
        }
        private static void CarAdded(object? sender, Car item)
        {
            Console.WriteLine($"{item.Brand} {item.Model} added");
            using (var writer = File.AppendText(auditFile))
            {
                writer.WriteLine($"[{DateTime.Now}]-CarAdded-[{item.Brand}{item.Model}]");
            }
        }

        private static void EmployeeRemoved(object? sender, Employee item)
        {
            Console.WriteLine($"{item.FirstName} {item.LastName} removed");
            using (var writer = File.AppendText(auditFile))
            {
                writer.WriteLine($"[{DateTime.Now}]-EmployeeDeleted-[{item.FirstName} {item.LastName}]");
            }
        }
        private static void ManagerRemoved(object? sender, Manager item)
        {
            Console.WriteLine($"{item.FirstName} {item.LastName} removed");
            using (var writer = File.AppendText(auditFile))
            {
                writer.WriteLine($"[{DateTime.Now}]-ManagerDeleted-[{item.FirstName} {item.LastName}]");
            }
        }
        private static void CarRemoved(object? sender, Car item)
        {
            Console.WriteLine($"{item.Brand} {item.Model} removed");
            using (var writer = File.AppendText(auditFile))
            {
                writer.WriteLine($"[{DateTime.Now}]-CarDeleted-[{item.Brand}{item.Model}]");
            }
        }

    }
}
