using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;
using System.Net.NetworkInformation;
using System.Text;

namespace MotoApp.Components.DataProviders
{
    public class CarsProvider : ICarsProvider
    {
        private readonly IRepository<Car> _carRepository;


        public CarsProvider(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public List<Car> GetCarsByBrand(string brand)
        {
            var cars = _carRepository.GetAll();
            return cars.Where(cars => cars.Brand == brand).ToList();
        }

        public List<Car> GetCarsFromMinimumPriceToMaximum(decimal minPrice, decimal maxPrice)
        {
            var cars = _carRepository.GetAll();
            return cars.Where(car => car.Price >= minPrice && car.Price <= maxPrice).ToList();
        }

        public List<Car> OrderByBrand()
        {
            var cars = _carRepository.GetAll();
            return cars.OrderBy(cars => cars.Brand).ToList();
        }
    }
}
