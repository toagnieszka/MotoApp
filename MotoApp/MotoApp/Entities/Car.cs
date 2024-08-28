using System.Text;

namespace MotoApp.Entities
{
    public class Car : EntityBase
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int ManufactureYear { get; set; }

        public decimal Price { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Brand} ({Model}), ID: {Id}");
            sb.AppendLine($"Kolor {Color}, Rok produkcji: {ManufactureYear}");
            sb.AppendLine($"Cena: {Price}zł");
            return sb.ToString();
        }
    }
}
