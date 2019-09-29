using Microsoft.AspNetCore.Components;
using StarshipTraveler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StartshipTraveler.Client.Data
{
    public class StarshipApi : IStarshipApi
    {
        private readonly HttpClient client;

        public StarshipApi(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = new Uri("http://localhost:5000/api/");
        }

        public async Task<Base> GetBaseAsync(string baseId) =>
            await client.GetJsonAsync<Base>($"bases/{baseId}");

        public async Task<IEnumerable<Base>> GetBasisAsync() =>
            await client.GetJsonAsync<List<Base>>($"bases");

        public async Task<IEnumerable<Connection>> GetConnectionsAsync() =>
            await client.GetJsonAsync<List<Connection>>($"connections");

        public async Task<Ticket> GetTicketAsync(string ticketId) =>
            await client.GetJsonAsync<Ticket>($"tickets/{ticketId}");

        public async Task<IEnumerable<Ticket>> GetTicketsAsync() =>
            await client.GetJsonAsync<List<Ticket>>($"tickets");

        public async Task PostTicket(Ticket ticket) =>
            await client.PostJsonAsync($"tickets", ticket);
    }
}
