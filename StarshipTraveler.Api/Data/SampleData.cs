﻿using StarshipTraveler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarshipTraveler.UI.Server.Data;

public static class SampleData
{
    public static Base[] Bases { get; } = new[]
    {
        new Base("Cybertron", "_content/StarshipTraveler.Components/images/cybertron.jpg"),
        new Base("Earth", "_content/StarshipTraveler.Components/images/earth.jpg"),
        new Base("Krypton", "_content/StarshipTraveler.Components/images/krypton.jpg"),
        new Base("Pandora", "_content/StarshipTraveler.Components/images/pandora.jpg"),
        new Base("Arrakis", "_content/StarshipTraveler.Components/images/arrakis.jpg"),
        new Base("Tatooine", "_content/StarshipTraveler.Components/images/tatooine.jpg"),
        new Base("Vulcan", "_content/StarshipTraveler.Components/images/vulcan.png"),
        new Base("Decapod 10", "_content/StarshipTraveler.Components/images/decapod-10.jpg"),
        new Base("Magrathea", "_content/StarshipTraveler.Components/images/magrathea.png")
    };

    public static Ticket[] Tickets { get; } = new[]
    {
        new Ticket { ID = "XE34AV6", From = "Cybertron", To = "Earth", Passenger = "Optimus Prime", Price = 499.99m, Date = new DateTime(2007, 7, 4) },
        new Ticket { ID = "U9XZLR9", From = "Cybertron", To = "Earth", Passenger = "Bumblebee", Price = 349.99m, Date = new DateTime(2005, 8, 20) },
        new Ticket { ID = "K40JS7V", From = "Krypton", To = "Earth", Passenger = "Kal-El", Price = 139.99m, Date = new DateTime(1947, 3, 20) },
        new Ticket { ID = "ACXC43O", From = "Earth", To = "Pandora", Passenger = "Jake Sully", Price = 89.99m, Date = new DateTime(2154, 10, 8) },
        new Ticket { ID = "ZA34SD0", From = "Decapod 10", To = "Earth", Passenger = "Dr. John Zoidberg", Price = 1999.99m, Date = new DateTime(2929, 1, 15) },
        new Ticket { ID = "M424242", From = "Earth", To = "Magrathea", Passenger = "Arthur Dent", Price = 4242.42m, Date = new DateTime(2042, 1, 31) }
    };

    public static Connection[] Connections { get; } = new[]
    {
        new Connection("Decapod 10", "Earth", 1000, 100m),
        new Connection("Cybertron", "Earth", 100, 375.75m),
        new Connection("Krypton", "Earth", 12300, 7499.99m),
        new Connection("Tatooine", "Vulcan", 5200, 89.90m),
        new Connection("Earth", "Pandora", 3750, 301.01m),
        new Connection("Earth", "Magrathea", 4242, 4242.42m),
        new Connection("Arrakis", "Decapod 10", 7300, 1349.90m),
        new Connection("Arrakis", "Pandora", 7300, 1349.90m),
        new Connection("Pandora", "Magrathea", 4242, 4242.42m),
        new Connection("Tatooine", "Krypton", 1234, 1345.67m),
    };
}
