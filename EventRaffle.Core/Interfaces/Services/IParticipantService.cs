using EventRaffle.Core.DTOs.Participant;
using EventRaffle.Core.Models;

namespace EventRaffle.Core.Interfaces.Services
{
    public interface IParticipantService
    {
        public Task<ResultModel<int>> UploadParticipantsAsync(Stream file, Guid eventId);
        public Task<ResultModel<IEnumerable<ParticipantDto>>> GetAllAsync(Guid eventId);
        public Task<ResultModel<bool>> RegisterAsync(Guid id);
    }
}
