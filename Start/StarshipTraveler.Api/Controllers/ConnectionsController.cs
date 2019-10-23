using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using StarshipTraveler.UI.Server.Data;
using StarshipTraveler.Model;

namespace StarshipTraveler.UI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConnectionsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Connection> Connections() =>
            SampleData.Connections.Union(SampleData.Connections.Select(c => new Connection
                {
                    From = c.To,
                    To = c.From,
                    Distance = c.Distance,
                    Price = c.Price
                })).ToArray();
    }
}
