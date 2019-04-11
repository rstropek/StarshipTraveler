using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StarshipTraveler.Model
{
    public class Base
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class GraphBase : Base
    {
        public static GraphBase[] BasesToGraphBases(Base[] bases) =>
            Enumerable.Range(0, bases.Length).Select(i => new GraphBase
            {
                GraphID = (uint)i,
                Name = bases[i].Name,
                Image = bases[i].Image
            }).ToArray();

        public uint GraphID { get; set; }
    }

    public class Ticket
    {
        [Required]
        public DateTime Date { get; set; }

        public string ID { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        public decimal Price { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters!")]
        public string Passenger { get; set; }

        public TimeRelation Time => Date >= DateTime.Today ? TimeRelation.Upcoming : TimeRelation.Past;
    }

    public enum TimeRelation
    {
        Past,
        Upcoming
    }

    public class Connection : IEquatable<Connection>
    {
        public string From { get; set; }
        public string To { get; set; }
        public int Distance { get; set; }
        public decimal Price { get; set; }
        
        public bool Equals(Connection other) =>
            From == other.From && To == other.To && Distance == other.Distance && Price == other.Price;
    }
}
