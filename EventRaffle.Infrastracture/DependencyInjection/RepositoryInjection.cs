using EventRaffle.Core.Interfaces.Repositories;
using EventRaffle.Infrastracture.Persistence;
using EventRaffle.Infrastracture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventRaffle.Infrastracture.DependencyInjection
{
    public static class RepositoryInjection
    {
        public static IServiceCollection AddInfrastracture(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IEventRepository, EventRepository>();

            services.AddDbContext<AppDbContext>(options =>                 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
