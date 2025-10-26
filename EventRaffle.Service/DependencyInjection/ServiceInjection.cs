using EventRaffle.Core.Interfaces.Services;
using EventRaffle.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventRaffle.Service.DependencyInjection
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<IEventService, EventService>();

            return services;
        }
    }
}
