using AutoMapper;
using EventRaffle.Service.Mappings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventRaffle.Service.DependencyInjection
{
    public static class MappingInjection
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            // Add AutoMapper or other mapping configurations here
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // You can add other providers like Debug, File, etc.
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EventProfile>();
                cfg.AddProfile<ParticipantProfile>();
            }, loggerFactory);

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
