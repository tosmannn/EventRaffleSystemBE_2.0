using AutoMapper;
using EventRaffle.Core.DTOs.Participant;
using EventRaffle.Core.Entities;

namespace EventRaffle.Service.Mappings
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<Participant, ParticipantDto>().ReverseMap();
        }

    }
}
