using EventRaffle.Core.Entities;
using EventRaffle.Core.Interfaces.Repositories;
using EventRaffle.Infrastracture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventRaffle.Infrastracture.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Event>> GetActiveRafflesAsync()
        {
            return await _dbSet
                .Where(e => e.IsActive && !e.IsDeleted)
                .ToListAsync();
        }
    }
}
