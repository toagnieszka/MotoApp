using MotoApp.Repositories;
using MotoApp.Entities;
using MotoApp.Data;
using MotoApp.Repositories.Extensions;


void EmployeeRepository_ItemAdded(object? sender, Employee e)
{
    Console.WriteLine($"Employee added => {e.FirstName} from {sender?.GetType().Name}");
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

Console.WriteLine("Witaj w MotoApp");
Console.WriteLine("==========================\n");
Console.WriteLine("Aplikacja służy do obsługi danych dotyczących pracowników oraz biznes partnerów\n\n");

var activeApp = true;

while (activeApp)

{

    Console.WriteLine("\nWybierz co chcesz zrobić, wpisując odpoweidni numer akcji:\n");
    Console.WriteLine("1 - Odczytać wszystkich pracowników\n2 - Dodać nowego pracownika\n3 - Usunąć pracownika\n" +
        "4 - Odczytać wszystkich menagerów\n5 - Dodać nowego menagera\n6 - Usunąć menagera\n" +
        "7 - Odczytać wszystkich partnerów biznesowych\n8 - Dodać nowego partnera biznesowego\n9 - Usunąć partnera biznesowego\n10 - Wyjść z aplikacji\n");

    var input1 = Console.ReadLine();
    float.TryParse(input1, out float userChoice);

    var employeeRepository = new SqlRepository<Employee>(new MotoAppDbContext(), EmployeeAdded);
    employeeRepository.ItemAdded += EmployeeRepository_ItemAdded;
    string name = null;

    if (userChoice == 1)
    {
        WriteAllToConsole(employeeRepository);
    }
    else if (userChoice == 2)
    {
        Console.WriteLine("\nPodaj imię pracownika:\n");
        var input2 = Console.ReadLine();
        input2 = name;
        AddEmployee(employeeRepository);
    }
    else if(userChoice == 3) 
    {
        Console.WriteLine("Którego pracownika chcesz usunąć?");

    }
    else if(userChoice == 4)
    {
        AddManager(employeeRepository);
    }


    void AddEmployee(IRepository<Employee> repository)
    {
        var employee = new[]
        {
       new Employee {FirstName = name},
   };

        repository.AddBatch(employee);
    }

    static void EmployeeAdded(Employee item)
    {
        Console.WriteLine($"{item.FirstName} added");
    }

   

    static void AddManager(IWriteRepository<Manager> managerRopsitory)
    {
        managerRopsitory.Add(new Manager { FirstName = "Franek" });
        managerRopsitory.Add(new Manager { FirstName = "Julek" });
        managerRopsitory.Add(new Manager { FirstName = "Ala" });
        managerRopsitory.Save();
    }


}