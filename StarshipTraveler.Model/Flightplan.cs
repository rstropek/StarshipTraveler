using System;
using System.Linq;

namespace StarshipTraveler.Model
{
    public class BasePoint
    {
        public Base Base { get; set; }
        public ConnectionWithBases[] DirectConnections { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public bool Active { get; private set; }
        public void SetActive(bool active = true)
        {
            foreach (var conn in DirectConnections)
            {
                conn.Active = active;
                conn.To.Active = active;
            }

            Active = active;
        }
    }

    public class ConnectionWithBases
    {
        public BasePoint From { get; set; }
        public BasePoint To { get; set; }
        public (double pFromX, double pFromY, double pToX, double pToY) ControlPoints { get; set; }
        public Connection Connection { get; set; }
        public bool Active { get; set; }
    }

    public interface IFlightplan
    {
        (BasePoint[] bases, ConnectionWithBases[] connections) PrepareFlightplan(Connection[] connectionTask, Base[] baseTask, (double X, double Y) center);
    }

    public class Flightplan : IFlightplan
    {
        public (BasePoint[] bases, ConnectionWithBases[] connections) PrepareFlightplan(Connection[] connectionTask, Base[] baseTask, (double X, double Y) center)
        {
            var angleStep = Math.PI * 2 / baseTask.Length;
            var points = Enumerable.Range(0, baseTask.Length)
                .Select(i => new BasePoint
                {
                    Base = baseTask[i],
                    X = 250 + Math.Sin(angleStep * i) * 250,
                    Y = 250 - Math.Cos(angleStep * i) * 250
                })
                .ToArray();
            var connections = connectionTask.Select(c => new ConnectionWithBases
            {
                Connection = c,
                From = points.First(b => b.Base.Name == c.From),
                To = points.First(b => b.Base.Name == c.To),
            }).ToArray();
            foreach(var conn in connections)
            {
                conn.ControlPoints = (conn.From.X + (center.X - conn.From.X) / 2,
                    conn.From.Y + (center.Y - conn.From.Y) / 2,
                    conn.To.X + (center.X - conn.To.X) / 2,
                    conn.To.Y + (center.Y - conn.To.Y) / 2);
            }

            foreach (var point in points)
            {
                point.DirectConnections = connections.Where(c => c.From == point).ToArray();
            }

            return (points, connections);
        }
    }
}
