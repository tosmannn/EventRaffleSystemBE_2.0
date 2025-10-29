using EventRaffle.Core.Entities;

namespace EventRaffle.Core.Interfaces.Repositories
{
    public interface IParticipantRepository : IBaseRepository<Participant>
    {
        Task<IEnumerable<Participant>> GetAllAsync(Guid eventId);
    }
}
