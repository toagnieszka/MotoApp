using MotoApp.Components.DataProviders;
using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;
using MotoApp.Data.Repositories.Extensions;

namespace MotoApp
{

    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Manager> _managerRepository;
        private readonly IRepository<Car> _carRepository;
        private readonly ICarsProvider _carsProvider;

        public UserCommunication(
            IRepository<Employee> employeeRepository,
            IRepository<Manager> managerRepository,
            IRepository<Car> carRepository,
            ICarsProvider carsProviderRepository)
        {
            _employeeRepository = employeeRepository;
            _managerRepository = managerRepository;
            _carRepository = carRepository;
            _carsProvider = carsProviderRepository;
        }
        public void UseUserCommunication()
        {
            var activeApp = true;
            string employeeName;
            string employeeLastName;
            string managerName;
            string managerLastName;
            string brand;
            string model;
            string color;
            int manufactureYear;
            decimal price;

            void AddEmployee()
            {
                _employeeRepository.ItemsToList();
                var employee = new[]
                {
                new Employee {FirstName = employeeName, LastName = employeeLastName},
            };

                _employeeRepository.AddBatch(employee);
            }

            void AddManager()
            {
                _managerRepository.ItemsToList();
                var manager = new[]
                 {
       new Manager {FirstName = managerName, LastName = managerLastName},
   };

                _managerRepository.AddBatch(manager);
            }

            void AddCar()
            {
                _carRepository.ItemsToList();
                var car = new[]
                {
                new Car {Id = 1,
                Brand = brand,
                Model = "C3",
                Color = "Niebieski",
                ManufactureYear = 1943,
                Price = 23499},
            };


                _carRepository.AddBatch(car);
            }

            static void WriteAllToConsole(IReadRepository<IEntity> repository)
            {
                repository.ItemsToList();
                var items = repository.GetAll();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    Console.WriteLine("W tej liście nie znajduje się jeszcze żaden element");
                }
            }

            void RemoveEmployee(int id)
            {
                _employeeRepository.ItemsToList();
                var items = _employeeRepository.GetAll();
                foreach (var item in items)
                {
                    if (id == item.Id)
                    {
                        _employeeRepository.Remove(item);
                    }
                }
                _employeeRepository.Save();
            }
            void RemoveManager(int id)
            {
                _managerRepository.ItemsToList();
                var items = _managerRepository.GetAll();
                foreach (var item in items)
                {
                    if (id == item.Id)
                    {
                        _managerRepository.Remove(item);
                    }
                }
                _managerRepository.Save();
            }
            void RemoveCar(int id)
            {
                _carRepository.ItemsToList();
                var items = _carRepository.GetAll();
                foreach (var item in items)
                {
                    if (id == item.Id)
                    {
                        _carRepository.Remove(item);
                    }
                }
                _carRepository.Save();
            }

            Console.WriteLine("Witaj w MotoApp");
            Console.WriteLine("==========================\n");
            Console.WriteLine("Aplikacja służy do obsługi danych dotyczących pracowników oraz biznes partnerów\n\n");

            while (activeApp)
            {
                Console.WriteLine("\nWybierz co chcesz zrobić, wpisując odpowiedni numer akcji:\n");
                Console.WriteLine("1 - Odczytać wszystkich pracowników\n2 - Dodać nowego pracownika\n3 - Usunąć pracownika\n" +
                    "4 - Odczytać wszystkich menagerów\n5 - Dodać nowego menagera\n6 - Usunąć menagera\n" +
                    "7 - Odczytać wszystke samochody\n8 - Dodać nowy samochód\n9 - Usunąć samochód\n10 - Użyć filtrów do wyszukiwania samochodów\n11 - Wyjść z aplikacji\n");

                float.TryParse(Console.ReadLine(), out float userChoice);

                if (userChoice == 1)
                {
                    WriteAllToConsole(_employeeRepository);
                }
                else if (userChoice == 2)
                {
                    Console.WriteLine("\nPodaj imię pracownika:\n");
                    employeeName = Console.ReadLine();
                    Console.WriteLine("\nPodaj nazwisko pracownika:\n");
                    employeeLastName = Console.ReadLine();
                    AddEmployee();
                }
                else if (userChoice == 3)
                {
                    Console.WriteLine("\nKtórego pracownika chcesz usunąć?\n");
                    WriteAllToConsole(_employeeRepository);
                    Console.WriteLine("\nWpisz jego ID:\n");
                    int id = int.Parse(Console.ReadLine());
                    RemoveEmployee(id);
                }
                else if (userChoice == 4)
                {
                    WriteAllToConsole(_managerRepository);
                }
                else if (userChoice == 5)
                {
                    Console.WriteLine("\nPodaj imię managera:\n");
                    managerName = Console.ReadLine();
                    Console.WriteLine("\nPodaj nazwisko managera:\n");
                    managerLastName = Console.ReadLine();
                    AddManager();
                }
                else if (userChoice == 6)
                {
                    Console.WriteLine("\nKtórego managera chcesz usunąć?\n");
                    WriteAllToConsole(_managerRepository);
                    Console.WriteLine("\nWpisz jego ID:\n");
                    int id = int.Parse(Console.ReadLine());
                    RemoveManager(id);
                }
                else if (userChoice == 7)
                {
                    WriteAllToConsole(_carRepository);
                }
                else if (userChoice == 8)
                {
                    Console.WriteLine("\nPodaj markę samochodu:\n");
                    brand = Console.ReadLine().ToUpper();
                    Console.WriteLine("\nPodaj model samochodu:\n");
                    model = Console.ReadLine().ToUpper();
                    Console.WriteLine("\nPodaj kolor samochodu:\n");
                    color = Console.ReadLine().ToUpper();
                    Console.WriteLine("\nPodaj rok produkcji samochodu:\n");
                    manufactureYear = int.Parse(Console.ReadLine());
                    Console.WriteLine("\nPodaj cenę samochodu:\n");
                    price = decimal.Parse(Console.ReadLine());
                    AddCar();
                }
                else if (userChoice == 9)
                {
                    Console.WriteLine("\nKtóry samochód chcesz usunąć?\n");
                    WriteAllToConsole(_carRepository);
                    Console.WriteLine("\nWpisz jego ID:\n");
                    int id = int.Parse(Console.ReadLine());
                    RemoveCar(id);
                }
                else if (userChoice == 10)
                {
                    while (true)
                    {
                        Console.WriteLine("\nWybierz co chcesz zrobić, wpisując odpowiedni numer akcji:\n");
                        Console.WriteLine("\n1 - Wyszukanie samochodów po marce\n2 - Wyszukanie samochodu w cenie od - do\n" +
                            "3 - Wyświetlić wszystkie samochody posegregowane markami\n4 - Powrót do głównego menu");
                        var userChoice2 = int.Parse(Console.ReadLine());
                        if (userChoice2 == 1)
                        {
                            Console.WriteLine("\nWpisz markę którą chcesz wyszukać:\n");
                            var carBrand = Console.ReadLine().ToUpper();
                            foreach (var car in _carsProvider.GetCarsByBrand(carBrand))
                            {
                                Console.WriteLine(car);
                            }
                        }
                        else if (userChoice2 == 2)
                        {
                            Console.WriteLine("\nWpisz minimalną cenę od której chcesz wuszykać samochód\n");
                            var minPrice = int.Parse(Console.ReadLine());
                            Console.WriteLine("\nWpisz maksymalną cenę od której chcesz wuszykać samochód\n");
                            var maxPrice = int.Parse(Console.ReadLine());
                            foreach (var car in _carsProvider.GetCarsFromMinimumPriceToMaximum(minPrice, maxPrice))
                            {
                                Console.WriteLine(car);
                            }
                        }
                        else if (userChoice2 == 3)
                        {
                            foreach (var car in _carsProvider.OrderByBrand())
                            {
                                Console.WriteLine(car);
                            }
                        }
                        else if (userChoice2 == 4)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Zły numer");
                        }
                    }
                }
                else if (userChoice == 11)
                {
                    activeApp = false;
                }
                else { Console.WriteLine("Niepoprawna wartość"); }
            }
        }
    }
}
