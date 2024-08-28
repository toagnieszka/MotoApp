using MotoApp.Entities;

namespace MotoApp.DataProviders
{
    public interface ICarsProvider
    {
        List<Car> GetCarsByBrand(string brand);

        List<Car> GetCarsFromMinimumPriceToMaximum(decimal minPrice, decimal maxPrice);

        List<Car> OrderByBrand();
    }
}
