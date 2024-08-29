using MotoApp.Data.Entities;

namespace MotoApp.Components.DataProviders
{
    public interface ICarsProvider
    {
        List<Car> GetCarsByBrand(string brand);

        List<Car> GetCarsFromMinimumPriceToMaximum(decimal minPrice, decimal maxPrice);

        List<Car> OrderByBrand();
    }
}
