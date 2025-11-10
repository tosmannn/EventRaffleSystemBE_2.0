using EventRaffle.Core.Entities;

namespace EventRaffle.Core.Interfaces.Repositories
{
    public interface IParticipantRepository : IBaseRepository<Participant>
    {
        Task<IEnumerable<Participant>> GetAllAsync(Guid eventId);
        Task<int> GetTotalParticipantsCountAsync(Guid eventId);
        Task<int> GetRegisteredParticipantsCountAsync(Guid eventId);
    }
}
