using EventRaffle.Core.Entities;
using EventRaffle.Core.Interfaces.Repositories;
using EventRaffle.Infrastracture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventRaffle.Infrastracture.Repositories
{
    public class RaffleRepository : BaseRepository<Raffle>, IRaffleRepository
    {
        public RaffleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Raffle>> GetUnselecteByEventId(Guid eventId)
        {
            return await _dbSet
                .Include(e => e.Participant)
                .Where(r => r.EventId == eventId && !r.IsSelected)
                .ToListAsync();
        }
    }
}
