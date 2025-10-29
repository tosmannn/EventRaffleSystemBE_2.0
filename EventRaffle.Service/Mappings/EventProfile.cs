using AutoMapper;
using EventRaffle.Core.DTOs.Event;
using EventRaffle.Core.Entities;

namespace EventRaffle.Service.Mappings
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Event, CreateEventDto>().ReverseMap();
            CreateMap<Event, EventNameAndIdDto>();
        }
    }
}
