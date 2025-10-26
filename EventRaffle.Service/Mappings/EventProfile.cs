using AutoMapper;
using EventRaffle.Core.DTOs.Event;
using EventRaffle.Core.Entities;

namespace EventRaffle.Service.Mappings
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            // Define your mapping configurations here
            CreateMap<Event, EventDto>();
        }
    }
}
