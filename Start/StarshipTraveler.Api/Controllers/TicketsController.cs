using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarshipTraveler.UI.Server.Data;
using StarshipTraveler.Model;
using Microsoft.Extensions.Logging;

namespace StarshipTraveler.UI.Server.Controllers
{
    [Route("api/[controller]")]
    public class TicketsController : Controller
    {
        private ILogger<TicketsController> Logger { get; set; }

        public TicketsController(ILogger<TicketsController> logger)
        {
            Logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Ticket>> Tickets()
        {
            // Slowing down things for demo purposes
            await Task.Delay(0 /*2000*/);

            return SampleData.Tickets;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Ticket([FromRoute] string id)
        {
            var ticket = SampleData.Tickets.FirstOrDefault(t => t.ID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult Ticket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                Logger.LogError("Model is invalid!");
                foreach(var m in ModelState.Values)
                {
                    foreach(var e in m.Errors)
                    {
                        Logger.LogError(e.ErrorMessage);
                    }
                }

                return BadRequest();
            }

            // Do something with ticket

            return StatusCode(201);
        }
    }
}
