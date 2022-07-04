using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarshipTraveler.UI.Server.Data;
using StarshipTraveler.Model;
using Microsoft.Extensions.Logging;

namespace StarshipTraveler.UI.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
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
    public IActionResult Ticket([FromBody] Ticket _)
    {
        // Do something with ticket

        return StatusCode(201);
    }
}
