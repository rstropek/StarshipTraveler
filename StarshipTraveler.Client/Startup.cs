using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using StarshipTraveler.Components;
using StarshipTraveler.Model;
using StartshipTraveler.Client.Data;

namespace StarshipTraveler.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFlightplan, Flightplan>();
            services.AddSingleton<IStarshipApi, StarshipApi>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
