using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;
using System;
using System.Linq;

namespace StarshipTraveler.Model;

public class BasePoint
{
    public Base? Base { get; set; }
    public ConnectionWithBases[]? DirectConnections { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public bool Active { get; private set; }
    public void SetActive(bool active = true)
    {
        if (DirectConnections != null)
        {
            foreach (var conn in DirectConnections)
            {
                if (conn != null && conn.To != null)
                {
                    conn.Active = active;
                    conn.To.Active = active;
                }
            }

            Active = active;
        }
    }
}

public class ConnectionWithBases
{
    public BasePoint? From { get; set; }
    public BasePoint? To { get; set; }
    public (double pFromX, double pFromY, double pToX, double pToY) ControlPoints { get; set; }
    public Connection? Connection { get; set; }
    public bool Active { get; set; }
}

public interface IFlightplan
{
    (BasePoint[] bases, ConnectionWithBases[] connections) PrepareFlightplan(Connection[] connections, Base[] bases, (double X, double Y) center);
    Base[] CalculateShortestPath(Connection[] connections, Base[] bases, string from, string to);
}

public class Flightplan : IFlightplan
{
    public (BasePoint[] bases, ConnectionWithBases[] connections) PrepareFlightplan(Connection[] connections, Base[] bases, (double X, double Y) center)
    {
        var angleStep = Math.PI * 2 / bases.Length;
        var points = Enumerable.Range(0, bases.Length)
            .Select(i => new BasePoint
            {
                Base = bases[i],
                X = 250 + Math.Sin(angleStep * i) * 250,
                Y = 250 - Math.Cos(angleStep * i) * 250
            })
            .ToArray();
        var connectionsWithBases = connections.Select(c => new ConnectionWithBases
        {
            Connection = c,
            From = points.First(b => b?.Base?.Name == c.From),
            To = points.First(b => b?.Base?.Name == c.To),
        }).ToArray();
        foreach(var conn in connectionsWithBases)
        {
            if (conn != null && conn.From != null && conn.To != null)
            {
                conn.ControlPoints = (conn.From.X + (center.X - conn.From.X) / 2,
                    conn.From.Y + (center.Y - conn.From.Y) / 2,
                    conn.To.X + (center.X - conn.To.X) / 2,
                    conn.To.Y + (center.Y - conn.To.Y) / 2);
            }
        }

        foreach (var point in points)
        {
            point.DirectConnections = connectionsWithBases.Where(c => c.From == point).ToArray();
        }

        return (points, connectionsWithBases);
    }

    public Base[] CalculateShortestPath(Connection[] connections, Base[] bases, string from, string to)
    {
        var graph = new Graph<GraphBase, Connection>();
        var graphBases = GraphBase.BasesToGraphBases(bases);

        foreach (var b in graphBases)
        {
            b.GraphID = graph.AddNode(b);
        }

        foreach (var c in connections)
        {
            var fromBase = graphBases.First(b => b.Name == c.From);
            var toBase = graphBases.First(b => b.Name == c.To);
            graph.Connect(fromBase.GraphID, toBase.GraphID, c.Distance, c);
            graph.Connect(toBase.GraphID, fromBase.GraphID, c.Distance, c);
        }

        var result = graph.Dijkstra(graphBases.First(b => b.Name == from).GraphID,
            graphBases.First(b => b.Name == to).GraphID);
        var steps = result.GetPath();
        return steps.Select(i => graphBases.First(b => b.GraphID == i)).ToArray();
    }
}
