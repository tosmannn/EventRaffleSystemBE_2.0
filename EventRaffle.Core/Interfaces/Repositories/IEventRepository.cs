using EventRaffle.Core.Entities;

namespace EventRaffle.Core.Interfaces.Repositories
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<IEnumerable<Event>> GetActiveRafflesAsync();
    }
}
