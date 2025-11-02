using EventRaffle.Core.DTOs.Raffle;
using EventRaffle.Core.Models;

namespace EventRaffle.Core.Interfaces.Services
{
    public interface IRaffleService
    {
        Task<ResultModel<RaffleDto>> DrawWinnerAsync(Guid eventId);
    }
}
