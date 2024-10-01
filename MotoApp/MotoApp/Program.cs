using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MotoApp;
using MotoApp.Components.CsvReader;
using MotoApp.Data;
using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;
using MotoApp.Services;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Employee>, ListRepository<Employee>>();
services.AddSingleton<IRepository<BusinessPartner>, ListRepository<BusinessPartner>>();
services.AddSingleton<IRepository<Car>, ListRepository<Car>>();
services.AddSingleton<IEventMethod, EventMethod>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IDbContextService, DbContextService>();
services.AddDbContext<MotoAppDbContext>(options => options
    .UseSqlServer("Data Source=LAPTOP-4G3C73B5\\SQLEXPRESS;Initial Catalog=MotoAppStorage;Integrated Security=True;TrustServerCertificate=True;"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
