using EventRaffle.Core.Entities;
using EventRaffle.Core.Interfaces.Repositories;
using EventRaffle.Infrastracture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventRaffle.Infrastracture.Repositories
{
    public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Participant>> GetAllAsync(Guid eventId)
        {
            return await _dbSet
                .Where(p => p.EventId == eventId)
                .ToListAsync();
        }

        public async Task<int> GetRegisteredParticipantsCountAsync(Guid eventId)
        {
            return await _dbSet
                .Where(p => p.EventId == eventId && p.IsRegistered)
                .CountAsync();
        }

        public async Task<int> GetTotalParticipantsCountAsync(Guid eventId)
        {
            return await _dbSet
                .Where(p => p.EventId == eventId)
                .CountAsync();
        }
    }
}
