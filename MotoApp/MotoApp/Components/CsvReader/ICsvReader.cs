using MotoApp.Components.CsvReader.Model;

namespace MotoApp.Components.CsvReader
{
    public interface ICsvReader
    {
        List<Car> ProcessCars(string filePath);

        List<Manufacturer> ProcessManufacturers(string filePath);
    }
}
