using StartshipTraveler.Model;
using System;
using System.Net.Http;

namespace StartshipTraveler.ServerSide.Data
{
    public class StarshipApi : StarshipApiBase
    {
        public StarshipApi(IHttpClientFactory factory)
        {
            var client = factory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5000/api/");
            Client = client;
        }
    }
}
