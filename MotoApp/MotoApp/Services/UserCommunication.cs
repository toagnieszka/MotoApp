using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;
using MotoApp.Data.Repositories.Extensions;

namespace MotoApp.Services
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IDbContextService _dbContextService;

        public UserCommunication(
            IRepository<Employee> employeeRepository,
            IDbContextService dbContextService)
        {
            _employeeRepository = employeeRepository;
            _dbContextService = dbContextService;
        }

        public void UseUserCommunication()
        {
            var activeApp = true;
            string employeeName;
            string employeeLastName;

            void AddEmployee()
            {
                _employeeRepository.ItemsToList();
                var employee = new[]
                {
                new Employee {FirstName = employeeName, LastName = employeeLastName},
            };

                _employeeRepository.AddBatch(employee);


            }

            
            _dbContextService.InsertCarData();
            _dbContextService.InsertBusinessPartnerData();
            _dbContextService.InsertEmployeeData("C:\\Users\\agnie\\Desktop\\projekty\\MotoApp\\MotoApp\\MotoApp\\bin\\Debug\\net7.0\\Employee.json");


            Console.WriteLine("Witaj w MotoApp");
            Console.WriteLine("==========================\n");
            Console.WriteLine("Aplikacja służy do obsługi danych dotyczących pracowników oraz biznes partnerów\n\n");

            while (activeApp)
            {
                Console.WriteLine("\nWybierz co chcesz zrobić, wpisując odpowiedni numer akcji:\n");
                Console.WriteLine("1 - Odczytać wszystkich pracowników\n2 - Dodać nowego pracownika\n3 - Usunąć pracownika\n" +
                    "4 - Zedytować pracownika\n5 - Odczytać wszystkich biznes partnerów\n6 - Usunąć biznes partnera\n" +
                    "7 - Odczytać wszystkie samochody\n8 - Usunąć samochód\n9 - Wyjść z aplikacji\n");

                var userChoice = int.Parse(Console.ReadLine());

                if (userChoice == 1)
                {
                    _dbContextService.ReadAllEmployeesFromDb();
                }
                else if (userChoice == 2)
                {
                    Console.WriteLine("\nPodaj imię pracownika:\n");
                    employeeName = Console.ReadLine();
                    Console.WriteLine("\nPodaj nazwisko pracownika:\n");
                    employeeLastName = Console.ReadLine();
                    _dbContextService.AddEmployeeToDb(employeeName, employeeLastName);
                }
                else if (userChoice == 3)
                {
                    _dbContextService.RemoveEmployee();
                }
                else if (userChoice == 4)
                {
                    _dbContextService.UpdateEmployeeData();
                }
                else if (userChoice == 5)
                {
                    _dbContextService.ReadAllBusinessPartnersFromDb();
                }
                else if (userChoice == 6)
                {
                    _dbContextService.RemoveBusinessPartner();
                }
                else if (userChoice == 7)
                {
                    _dbContextService.ReadAllCarsFromDb();
                }
                else if (userChoice == 8)
                {
                    _dbContextService.RemoveCar();
                }
                else if (userChoice == 9)
                {
                    activeApp = false;
                }
                else { Console.WriteLine("Niepoprawna wartość"); }
            }
        }
    }
}
