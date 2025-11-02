using AutoMapper;
using EventRaffle.Core.DTOs.Raffle;
using EventRaffle.Core.Entities;

namespace EventRaffle.Service.Mappings
{
    public class RaffleProfile : Profile
    {
        public RaffleProfile()
        {
            CreateMap<Participant, RaffleDto>();
        }
    }
}
