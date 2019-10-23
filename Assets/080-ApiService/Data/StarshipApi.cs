using StartshipTraveler.Model;
using System;
using System.Net.Http;

namespace StartshipTraveler.Client.Data
{
    public class StarshipApi : StarshipApiBase
    {
        public StarshipApi(HttpClient client)
        {
            client.BaseAddress = new Uri("http://localhost:5000/api/");
            Client = client;
        }
    }
}
