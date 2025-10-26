using EventRaffle.Core.Interfaces.Repositories;
using EventRaffle.Core.Interfaces.Services;

namespace EventRaffle.Service.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }


    }
}
