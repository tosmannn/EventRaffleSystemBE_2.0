using AutoMapper;
using EventRaffle.Core.DTOs.Raffle;
using EventRaffle.Core.Entities;
using EventRaffle.Core.Interfaces.Repositories;
using EventRaffle.Core.Interfaces.Services;
using EventRaffle.Core.Interfaces.Time;
using EventRaffle.Core.Models;
using Microsoft.Extensions.Logging;

namespace EventRaffle.Service.Services
{
    public class RaffleService : IRaffleService
    {
        private readonly ILogger<RaffleService> _logger;
        private readonly IMapper _mapper;

        private readonly IRaffleRepository _raffleRepository;
        private readonly IClock _clock;
        private static readonly Random _random = new();

        private const string LogPrefix = "[RaffleService]";
        private string Prefix(string methodName) => $"{LogPrefix} [{methodName}]";


        public RaffleService(ILogger<RaffleService> logger
            , IMapper mapper
            , IParticipantRepository participantRepository
            , IRaffleRepository raffleRepository
            , IClock clock)
        {
            _logger = logger;
            _mapper = mapper;
            _raffleRepository = raffleRepository;
            _clock = clock;
        }

        public async Task<ResultModel<RaffleDto>> DrawWinnerAsync(Guid eventId)
        {
            var methodName = nameof(DrawWinnerAsync);

            _logger.LogInformation("{Prefix} Start Draw winner", Prefix(methodName));

            try
            {
                var raffleEntries = await _raffleRepository.GetUnselecteByEventId(eventId);

                if (!raffleEntries.Any())
                {
                    return ResultModel<RaffleDto>.Ok(null, "No Raffle entries found, please register");
                }
                
                var winner = raffleEntries[_random.Next(raffleEntries.Count)];

                winner.IsSelected = true;
                winner.ModifiedUtcDate = _clock.UtcNow;

                await _raffleRepository.SaveChangesAsync();

                var raffleDto = _mapper.Map<RaffleDto>(winner.Participant);

                raffleDto.Count = raffleEntries.Count - 1;

                _logger.LogInformation("{Prefix} Raffle contenders left: {count}", Prefix(methodName), raffleDto.Count);
                if (raffleDto.Count == 0)
                    return ResultModel<RaffleDto>.Ok(raffleDto, $"All raffle selected");

                _logger.LogInformation("{Prefix} Winner: {winner}. Raffle Id: {id}", Prefix(methodName), raffleDto.FullName, winner.Id);

                return ResultModel<RaffleDto>.Ok(raffleDto, $"Your winner is {raffleDto.FullName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Prefix} Error Draw winner", Prefix(methodName));
                return ResultModel<RaffleDto>.Fail("An error occurred while drawing winner.", 500, new List<string> { ex.Message });
            }
            finally
            {
                _logger.LogInformation("{Prefix} Finished draw winner.", Prefix(methodName));
            }

        }

    }
}
