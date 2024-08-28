using Microsoft.Extensions.DependencyInjection;
using MotoApp;
using MotoApp.DataProviders;
using MotoApp.Entities;
using MotoApp.Repositories;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Employee>, ListRepository<Employee>>();
services.AddSingleton<IRepository<Manager>, ListRepository<Manager>>();
services.AddSingleton<IRepository<Car>, ListRepository<Car>>();
services.AddSingleton<ICarsProvider, CarsProvider>();
services.AddSingleton<IEventMethod, EventMethod>();
services.AddSingleton<IUserCommunication, UserCommunication>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
