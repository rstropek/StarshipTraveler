using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarshipTraveler.Model;

public interface IStarshipApi
{
    Task<IEnumerable<Base>> GetBasisAsync();

    Task<IEnumerable<Connection>> GetConnectionsAsync();

    Task PostTicket(Ticket ticket);

    Task<Ticket> GetTicketAsync(string ticketId);
    
    Task<IEnumerable<Ticket>> GetTicketsAsync();

    Task<Base> GetBaseAsync(string baseId);
}
