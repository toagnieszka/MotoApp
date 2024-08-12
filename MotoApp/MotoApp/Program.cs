using MotoApp.Repositories;
using MotoApp.Entities;
using MotoApp.Data;
using MotoApp.Repositories.Extensions;

#region Global Variables
var activeApp = true;
string employeeName = null;
string managerName = null;
string businessPartnerName = null;
#endregion

#region Repositories
var employeeRepository = new SqlRepository<Employee>(new MotoAppDbContext(), EmployeeAdded);
var managerRepository = new SqlRepository<Manager>(new MotoAppDbContext());
var businessPartnerRepository = new SqlRepository<BusinessPartner>(new MotoAppDbContext());

employeeRepository.ItemAdded += EmployeeRepository_ItemAdded;

void EmployeeRepository_ItemAdded(object? sender, Employee e)
{
    Console.WriteLine($"Employee added => {e.FirstName} from {sender?.GetType().Name}");
}
#endregion

#region Console
static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
} 
#endregion

void IterAll(SqlRepository<IEntity> repository, int id)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        if (id == item.Id)
        {
            repository.Remove(item);
        }
    }
}

Console.WriteLine("Witaj w MotoApp");
Console.WriteLine("==========================\n");
Console.WriteLine("Aplikacja służy do obsługi danych dotyczących pracowników oraz biznes partnerów\n\n");


while (activeApp)
{
    Console.WriteLine("\nWybierz co chcesz zrobić, wpisując odpowiedni numer akcji:\n");
    Console.WriteLine("1 - Odczytać wszystkich pracowników\n2 - Dodać nowego pracownika\n3 - Usunąć pracownika\n" +
        "4 - Odczytać wszystkich menagerów\n5 - Dodać nowego menagera\n6 - Usunąć menagera\n" +
        "7 - Odczytać wszystkich partnerów biznesowych\n8 - Dodać nowego partnera biznesowego\n9 - Usunąć partnera biznesowego\n10 - Wyjść z aplikacji\n");

    float.TryParse(Console.ReadLine(), out float userChoice);

    if (userChoice == 1)
    {
        WriteAllToConsole(employeeRepository);
    }
    else if (userChoice == 2)
    {
        Console.WriteLine("\nPodaj imię pracownika:\n");
        employeeName = Console.ReadLine();
        AddEmployee(employeeRepository);
    }
    else if (userChoice == 3)
    {
        Console.WriteLine("Którego pracownika chcesz usunąć?\n");
        WriteAllToConsole(employeeRepository);
        Console.WriteLine("\nWpisz jego ID:");
        var input = Console.ReadLine();
        int id = int.Parse(input);
        IterAll(employeeRepository, id);
    }
    else if (userChoice == 4)
    {
        Console.WriteLine("\nPodaj imię pracownika:\n");
        managerName = Console.ReadLine();
        AddManager(managerRepository);
    }
}

    void AddEmployee(IRepository<Employee> repository)
    {
        var employee = new[]
        {
       new Employee {FirstName = employeeName},
   };

        repository.AddBatch(employee);
    }

    static void EmployeeAdded(Employee item)
    {
        Console.WriteLine($"{item.FirstName} added");
    }

    void AddManager(IRepository<Manager> repository)
    {
        var manager = new[]
         {
       new Manager {FirstName = managerName},
   };

        repository.AddBatch(manager);
    }

    void AddBusinessPartner(IRepository<BusinessPartner> repository)
    {
        var businessPartner = new[]
         {
       new BusinessPartner {Name = businessPartnerName},
   };

        repository.AddBatch(businessPartner);
    }


