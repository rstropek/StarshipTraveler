using Microsoft.Extensions.Hosting;
using StartshipTraveler.Model;
using System;
using System.Net.Http;

namespace StarshipTraveler.ServerSide.Data
{
    public class StarshipApi : StarshipApiBase
    {
        public StarshipApi(IHttpClientFactory factory, IHostEnvironment hostEnvironment)
        {
            var apiUrl = hostEnvironment.IsDevelopment() ? "http://localhost:5000/api/" : "https://starshipapi.azurewebsites.net/api/";

            var client = factory.CreateClient();
            client.BaseAddress = new Uri(apiUrl);
            Client = client;
        }
    }
}
