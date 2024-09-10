using MotoApp.Components.CsvReader;
using MotoApp.Components.DataProviders;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace MotoApp
{
    public class App : IApp
    {
        private readonly IEventMethod _eventMethod;
        private readonly IUserCommunication _userCommunication;
        private readonly ICsvReader _csvReader;

        public App(
            IEventMethod eventsHandler,
            IUserCommunication userCommunication,
            ICsvReader csvReader)
        {
            _eventMethod = eventsHandler;
            _userCommunication = userCommunication;
            _csvReader = csvReader;
        }

        public void Run()
        {
            var manufacturers = _csvReader.ProcessManufacturers("Resources\\Files\\manufacturers.csv");
            var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");

            CreateXml();

            var carByName = cars.Join(
                manufacturers,
                x => x.Manufacturer,
                x => x.Name,
                (car, manufacturer) =>
                new
                {
                    manufacturer.Name,
                    manufacturer.Country,
                    car.Combined,
                });

            _eventMethod.UseEvents();
            _userCommunication.UseUserCommunication();
        }

        private void CreateXml()
        {
            var manufacturers = _csvReader.ProcessManufacturers("Resources\\Files\\manufacturers.csv");
            var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");

            var groups = manufacturers.GroupJoin(
                cars,
                m => m.Name,
                c => c.Manufacturer,
                (m, c) =>
                new
                {
                    Cars = c,
                    Manufacturer = m
                });

            var document = new XDocument(new XElement("Manufacturers"));
            foreach (var group in groups)
            {
                var elements = new XElement("Manufacturers", 
                    new XElement("Manufacturer",
                    new XAttribute("Name", group.Manufacturer.Name),
                    new XAttribute("Country", group.Manufacturer.Country),
                        new XElement("Cars",
                        new XAttribute("Country",group.Manufacturer.Country),
                        new XAttribute("CombinedSum", group.Cars.Sum(x => x.Combined)),
                            group.Cars.Select (x =>
                            new XElement("Car",
                            new XAttribute("CarModel",x.Name),
                            new XAttribute("Combined", x.Combined))
                        ))));

                document.Root.Add(elements);
            }
            
            document.Save("manufacturers&fuel.xml");
        }
    }
}
