using StartshipTraveler.Model;

namespace StarshipTraveler.ServerSide.Data;

public class StarshipApi : StarshipApiBase
{
    public StarshipApi(IHttpClientFactory factory, IHostEnvironment hostEnvironment)
        : base(factory.CreateClient())
    {
        var apiUrl = hostEnvironment.IsDevelopment() ? "http://localhost:5000/api/" : "https://starshipapi.azurewebsites.net/api/";
        Client.BaseAddress = new Uri(apiUrl);
    }
}
