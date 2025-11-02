using EventRaffle.Core.Entities;

namespace EventRaffle.Core.Interfaces.Repositories
{
    public interface IRaffleRepository : IBaseRepository<Raffle>
    {
        Task<List<Raffle>> GetUnselecteByEventId(Guid eventId);
    }
}
