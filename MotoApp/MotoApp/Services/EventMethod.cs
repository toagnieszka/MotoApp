using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;

namespace MotoApp
{
    public class EventMethod : IEventMethod
    {
        const string auditFile = "AuditFile.txt";

        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<BusinessPartner> _businessPartnerRepository;
        private readonly IRepository<Car> _carRepository;

        public EventMethod(
            IRepository<Employee> employeeRepository,
            IRepository<BusinessPartner> businessPartnerRepository,
            IRepository<Car> carRepository)
        {
            _employeeRepository = employeeRepository;
            _businessPartnerRepository = businessPartnerRepository;
            _carRepository = carRepository;
        }
        public void UseEvents()
        {
            _employeeRepository.ItemAdded += EmployeeAdded;
            _employeeRepository.ItemRemoved += EmployeeRemoved;
            _businessPartnerRepository.ItemRemoved += BusinessPartnerRemoved;
        }
        private static void EmployeeAdded(object? sender, Employee item)
        {
            Console.WriteLine($"{item.FirstName} {item.LastName} added");
            using (var writer = File.AppendText(auditFile))
            {
                writer.WriteLine($"[{DateTime.Now}]-EmployeeAdded-[{item.FirstName} {item.LastName}]");
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
        private static void BusinessPartnerRemoved(object? sender, BusinessPartner item)
        {
            Console.WriteLine($"{item.Name} {item.Id} removed");
            using (var writer = File.AppendText(auditFile))
            {
                writer.WriteLine($"[{DateTime.Now}]-BusinessPartnerDeleted-[{item.Name} {item.Id}]");
            }
        }
    }
}
