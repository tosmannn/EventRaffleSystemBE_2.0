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

        public async Task<bool> EventNameExistAsync(string name)
        {
            return await _dbSet
                .AnyAsync(e => EF.Functions.Like(e.Name, name) && !e.IsDeleted);
        }

        public async Task<Event?> GetActiveEventAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(e => e.IsActive && !e.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Event>> GetAllActiveEventsAsync()
        {
           return await _dbSet
                .Where(e => e.IsActive && !e.IsDeleted)
                .ToListAsync();
        }
    }
}
