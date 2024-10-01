using MotoApp.Data.Entities;

namespace MotoApp.Services
{
    public interface IDbContextService
    {
        void InsertBusinessPartnerData();
        void InsertCarData();
        void InsertEmployeeData(string file);
        void ReadAllCarsFromDb();
        void ReadAllBusinessPartnersFromDb();
        void ReadAllEmployeesFromDb();
        Car? FindCarById(int id);
        BusinessPartner? FindBusinessPartnerById(int id);
        Employee? FindEmployeeById(int id);
        void RemoveCar();
        void RemoveBusinessPartner();
        void RemoveEmployee();
        void UpdateEmployeeData();
        void AddEmployeeToDb(string firstName, string lastName);
    }
}
