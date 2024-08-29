namespace MotoApp.Data.Entities
{
    public class Manager : Employee
    {
        public override string ToString()
        {
            return base.ToString() + "(Manager)";
        }
    }
}
