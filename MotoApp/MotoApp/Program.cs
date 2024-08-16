using MotoApp.Repositories;
using MotoApp.Entities;
using MotoApp.Data;
using MotoApp.Repositories.Extensions;

#region Global Variables
var activeApp = true;
string employeeName = null;
string employeeLastName = null;
string managerName = null;
string managerLastName = null;
string businessPartnerName = null;
var employeeRepository = new ListRepository<Employee>();
var managerRepository = new ListRepository<Manager>();
var businessPartnerRepository = new ListRepository<BusinessPartner>();
const string auditFile = "AuditFile.json";
#endregion

#region Events
employeeRepository.ItemAdded += EmployeeAdded;
managerRepository.ItemAdded += ManagerAdded;
businessPartnerRepository.ItemAdded += BusinessPartnerAdded;

static void EmployeeAdded(object? sender, Employee item)
{
    Console.WriteLine($"{item.FirstName} {item.LastName} added");
    string json = $"[{DateTime.Now}]-EmployeeAdded-[{item.FirstName} {item.LastName}]";
    File.WriteAllText( auditFile, json );
}
static void ManagerAdded(object? sender, Manager item)
{
    Console.WriteLine($"{item.FirstName} {item.LastName} added");
    string json = $"[{DateTime.Now}]-ManagerAdded-[{item.FirstName} {item.LastName}]";
    File.WriteAllText(auditFile, json);
}
static void BusinessPartnerAdded(object? sender, BusinessPartner item)
{
    Console.WriteLine($"{item.Name} added");
    string json = $"[{DateTime.Now}]-BusinessPartnerAdded-[{item.Name}]";
    File.WriteAllText(auditFile, json);
}

employeeRepository.ItemRemoved += EmployeeRemoved;
managerRepository.ItemRemoved += ManagerRemoved;
businessPartnerRepository.ItemRemoved += BusinessPartnerRemoved;

static void EmployeeRemoved(object? sender, Employee item)
{
    Console.WriteLine($"{item.FirstName} {item.LastName} removed");
    string json = $"[{DateTime.Now}]-EmployeeDeleted-[{item.FirstName} {item.LastName}]";
    File.WriteAllText(auditFile, json);
}
static void ManagerRemoved(object? sender, Manager item)
{
    Console.WriteLine($"{item.FirstName} {item.LastName} removed");
    string json = $"[{DateTime.Now}]-ManagerDeleted-[{item.FirstName} {item.LastName}]";
    File.WriteAllText(auditFile, json);
}
static void BusinessPartnerRemoved(object? sender, BusinessPartner item)
{
    Console.WriteLine($"{item.Name} removed");
    string json = $"[{DateTime.Now}]-BusinessPartnerDeleted-[{item.Name}]";
    File.WriteAllText(auditFile, json);
}
#endregion

#region Add Methods
void AddEmployee(IRepository<Employee> repository)
{
    repository.ItemsToList();
    var employee = new[]
    {
                new Employee {FirstName = employeeName, LastName = employeeLastName},
            };

    repository.AddBatch(employee);
}



void AddManager(IRepository<Manager> repository)
{
    repository.ItemsToList();
    var manager = new[]
     {
       new Manager {FirstName = managerName, LastName = managerLastName},
   };

    repository.AddBatch(manager);
}

void AddBusinessPartner(IRepository<BusinessPartner> repository)
{
    repository.ItemsToList();
    var businessPartner = new[]
     {
       new BusinessPartner {Name = businessPartnerName},
   };

    repository.AddBatch(businessPartner);
}
#endregion

#region Other Methods
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

void RemoveById<T>(ListRepository<T> repository, int id) where T : class, IEntity
{
    repository.ItemsToList();
    var items = repository.GetAll();
    foreach (var item in items)
    {
        if (id == item.Id)
        {
            repository.Remove(item);
        }
    }
    repository.Save();
}
#endregion

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
        Console.WriteLine("\nPodaj nazwisko pracownika:\n");
        employeeLastName = Console.ReadLine();
        AddEmployee(employeeRepository);
    }
    else if (userChoice == 3)
    {
        Console.WriteLine("\nKtórego pracownika chcesz usunąć?\n");
        WriteAllToConsole(employeeRepository);
        Console.WriteLine("\nWpisz jego ID:\n");
        int id = int.Parse(Console.ReadLine());
        RemoveById(employeeRepository, id);
    }
    else if (userChoice == 4)
    {
        WriteAllToConsole(managerRepository);
    }
    else if (userChoice == 5)
    {
        Console.WriteLine("\nPodaj imię managera:\n");
        managerName = Console.ReadLine();
        Console.WriteLine("\nPodaj nazwisko managera:\n");
        managerLastName = Console.ReadLine();
        AddManager(managerRepository);
    }
    else if (userChoice == 6)
    {
        Console.WriteLine("\nKtórego managera chcesz usunąć?\n");
        WriteAllToConsole(managerRepository);
        Console.WriteLine("\nWpisz jego ID:\n");
        int id = int.Parse(Console.ReadLine());
        RemoveById(managerRepository, id);
    }
    else if (userChoice == 7)
    {
        WriteAllToConsole(businessPartnerRepository);
    }
    else if (userChoice == 8)
    {
        Console.WriteLine("\nPodaj nazwę biznes partnera:\n");
        businessPartnerName = Console.ReadLine();
        AddBusinessPartner(businessPartnerRepository);
    }
    else if (userChoice == 9)
    {
        Console.WriteLine("\nKtórego biznes partnera chcesz usunąć?\n");
        WriteAllToConsole(businessPartnerRepository);
        Console.WriteLine("\nWpisz jego ID:\n");
        int id = int.Parse(Console.ReadLine());
        RemoveById(businessPartnerRepository, id);
    }
    else if (userChoice == 10) 
    {
        activeApp = false;
    }
    else { Console.WriteLine("Niepoprawna wartość"); }
}