using EventRaffle.Core.Interfaces.Services;
using EventRaffle.Core.Interfaces.Time;
using EventRaffle.Infrastracture.Utilities;
using EventRaffle.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventRaffle.Service.DependencyInjection
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<IRaffleService, RaffleService>();

            services.AddSingleton<IClock, SystemClock>();

            return services;
        }
    }
}
