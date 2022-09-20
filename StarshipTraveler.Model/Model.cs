using System.ComponentModel.DataAnnotations;

namespace StarshipTraveler.Model;

public record Base(string Name, string Image);

public record GraphBase : Base
{
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

public record Connection(string From, string To, int Distance, decimal Price);
