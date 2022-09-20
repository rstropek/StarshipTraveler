using StarshipTraveler.Model;
using System.Text;
using System.Text.Json;

namespace StartshipTraveler.Model;

public abstract class StarshipApiBase : IStarshipApi
{
    private readonly HttpClient client;

    public StarshipApiBase(HttpClient client)
    {
        this.client = client;
    }

    protected HttpClient Client => client;

    public async Task<Base> GetBaseAsync(string baseId)
    {
        var response = await client.GetAsync($"bases/{baseId}");
        var baseJson = await response.Content.ReadAsStringAsync();
        var foundBase = JsonSerializer.Deserialize<Base>(baseJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });

        if (foundBase == null)
        {
            throw new InvalidOperationException("Could not deserialize JSON. Should never happen!");
        }

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

        if (bases == null)
        {
            throw new InvalidOperationException("Could not deserialize JSON. Should never happen!");
        }

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

        if (connections == null)
        {
            throw new InvalidOperationException("Could not deserialize JSON. Should never happen!");
        }

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

        if (ticket == null)
        {
            throw new InvalidOperationException("Could not deserialize JSON. Should never happen!");
        }

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

        if (tickets == null)
        {
            throw new InvalidOperationException("Could not deserialize JSON. Should never happen!");
        }

        return tickets;
    }

    public async Task PostTicket(Ticket ticket)
    {
        var content = new StringContent(JsonSerializer.Serialize(ticket), Encoding.UTF8, "application/json");
        await client.PostAsync($"tickets", content);
    }
}
