using EventRaffle.Core.Entities;

namespace EventRaffle.Core.Interfaces.Repositories
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<Event?> GetActiveEventAsync();
        Task<bool> EventNameExistAsync(string name);
        Task<IEnumerable<Event>> GetAllActiveEventsAsync();
    }

}
