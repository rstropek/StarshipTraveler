using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StartshipTraveler.Model;
using System;
using System.Net.Http;

namespace StartshipTraveler.Client.Data
{
    public class StarshipApi : StarshipApiBase
    {
        public StarshipApi(HttpClient client, IWebAssemblyHostEnvironment hostEnvironment)
        {
            // Note Blazor environment. For more details see
            // https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/environments?view=aspnetcore-5.0
            var apiUrl = hostEnvironment.IsDevelopment() ? "http://localhost:5000/api/" : "https://starshipapi.azurewebsites.net";

            client.BaseAddress = new Uri(apiUrl);
            Client = client;
        }
    }
}
