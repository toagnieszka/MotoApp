using MotoApp.Repositories;
using MotoApp.Repositories.Extensions;

namespace MotoApp.Entities
{
    public class Employee : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        { return $"First name: {FirstName}, Last name: {LastName} ID: {Id}"; }

        

        
    }
}
