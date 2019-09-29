using StarshipTraveler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StartshipTraveler.ServerSide.Data
{
    public class StarshipApi : IStarshipApi
    {
        private readonly HttpClient client;

        public StarshipApi(IHttpClientFactory factory)
        {
            client = factory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5000/api/");
        }

        public async Task<Base> GetBaseAsync(string baseId)
        {
            var response = await client.GetAsync($"bases/{baseId}");
            var baseJson = await response.Content.ReadAsStringAsync();
            var foundBase = JsonSerializer.Deserialize<Base>(baseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return foundBase;
        }

        public async Task<IEnumerable<Base>> GetBasisAsync()
        {
            var response = await client.GetAsync($"bases");
            var basesJson = await response.Content.ReadAsStringAsync();
            var bases = JsonSerializer.Deserialize<List<Base>>(basesJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return bases;
        }

        public async Task<IEnumerable<Connection>> GetConnectionsAsync()
        {
            var response = await client.GetAsync($"connections");
            var connectionsJson = await response.Content.ReadAsStringAsync();
            var connections = JsonSerializer.Deserialize<List<Connection>>(connectionsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return connections;
        }

        public async Task<Ticket> GetTicketAsync(string ticketId)
        {
            var response = await client.GetAsync($"tickets/{ticketId}");
            var ticketJson = await response.Content.ReadAsStringAsync();
            var ticket = JsonSerializer.Deserialize<Ticket>(ticketJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            var response = await client.GetAsync($"tickets");
            var ticketsJson = await response.Content.ReadAsStringAsync();
            var tickets = JsonSerializer.Deserialize<List<Ticket>>(ticketsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return tickets;
        }

        public async Task PostTicket(Ticket ticket)
        {
            var content = new StringContent(JsonSerializer.Serialize(ticket), Encoding.UTF8, "application/json");
            await client.PostAsync($"tickets", content);
        }
    }
}
