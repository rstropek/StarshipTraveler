using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using StarshipTraveler.UI.Server.Data;
using System.Linq;
using StarshipTraveler.Model;

namespace StarshipTraveler.UI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Base> Bases() => SampleData.Bases;

        [HttpGet]
        [Route("{name}")]
        public IActionResult Base([FromRoute] string name)
        {
            var baseObj = SampleData.Bases.FirstOrDefault(b => b.Name == name);
            if (baseObj == null)
            {
                return NotFound();
            }

            return Ok(baseObj);
        }
    }
}
