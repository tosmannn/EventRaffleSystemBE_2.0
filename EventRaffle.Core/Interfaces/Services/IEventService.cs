using EventRaffle.Core.DTOs.Event;
using EventRaffle.Core.Models;

namespace EventRaffle.Core.Interfaces.Services
{
    public interface IEventService
    {
        Task<ResultModel<EventDto>> CreateEventAsync(CreateEventDto dto);
        Task<ResultModel<bool>> ValidateEventNameAsync(string eventName);
        Task<ResultModel<EventNameAndIdDto?>> GetActiveEventNameAndIdAsync();
    }
}
