namespace MotoApp.Entities
{
    public class Employee : EntityBase
    {
        public string FirstName { get; set; }

        public override string ToString()
        { return $"First name: {FirstName}, ID: {Id}"; }
    }
}
