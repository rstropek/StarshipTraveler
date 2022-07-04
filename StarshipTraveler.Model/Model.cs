using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StarshipTraveler.Model;

public class Base
{
    public string? Name { get; set; }
    public string? Image { get; set; }

    public Base() { }

    public Base(string name, string image)
    {
        Name = name;
        Image = image;
    }
}

public class GraphBase : Base
{
    public GraphBase() { }

    public GraphBase(uint graphID, string name, string image) : base(name, image)
    {
        GraphID = graphID;
    }

    public static GraphBase[] BasesToGraphBases(Base[] bases) =>
        Enumerable.Range(0, bases.Length).Select(i =>
            new GraphBase((uint)i, bases[i].Name ?? string.Empty, bases[i].Image ?? string.Empty)).ToArray();

    public uint GraphID { get; set; }
}

public class Ticket
{
    [Required]
    public DateTime Date { get; set; }

    public string ID { get; set; } = string.Empty;

    [Required]
    public string From { get; set; } = string.Empty;

    [Required]
    public string To { get; set; } = string.Empty;

    public decimal Price { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters!")]
    public string Passenger { get; set; } = string.Empty;

    public TimeRelation Time => Date >= DateTime.Today ? TimeRelation.Upcoming : TimeRelation.Past;
}

public enum TimeRelation
{
    Past,
    Upcoming
}

public class Connection : IEquatable<Connection>
{
    public string? From { get; set; }
    public string? To { get; set; }
    public int Distance { get; set; }
    public decimal Price { get; set; }

    public bool Equals(Connection other) =>
        From == other.From && To == other.To && Distance == other.Distance && Price == other.Price;
}
