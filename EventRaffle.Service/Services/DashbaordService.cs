using AutoMapper;
using EventRaffle.Core.DTOs.Dashboard;
using EventRaffle.Core.Interfaces.Repositories;
using EventRaffle.Core.Interfaces.Services;
using EventRaffle.Core.Interfaces.Time;
using EventRaffle.Core.Models;
using Microsoft.Extensions.Logging;

namespace EventRaffle.Service.Services
{
    public class DashbaordService : IDashbaordService
    {
        private readonly ILogger<DashbaordService> _logger;

        private readonly IParticipantRepository _participantRepository;
        private readonly IEventRepository _eventRepository;

        private const string LogPrefix = "[DashbaordService]";
        private string Prefix(string methodName) => $"{LogPrefix} [{methodName}]";
        public DashbaordService(ILogger<DashbaordService> logger
                              , IParticipantRepository participantRepository
                              , IEventRepository eventRepository) 
        { 
            _logger = logger;
            _participantRepository = participantRepository;
            _eventRepository = eventRepository;

        }
        public async Task<ResultModel<DashboardDto>> GetDashboardSummary()
        {
            const string methodName = nameof(GetDashboardSummary);
            _logger.LogInformation("{Prefix} Start", Prefix(methodName));
            try
            {
                var currentEvent = await _eventRepository.GetActiveEventAsync();
                if (currentEvent == null)
                {
                    _logger.LogWarning("{Prefix} No current event found", Prefix(methodName));
                    return ResultModel<DashboardDto>.Fail("No current event found", 404);
                }
                var totalParticipants = await _participantRepository.GetTotalParticipantsCountAsync(currentEvent.Id);
                var registeredParticipants = await _participantRepository.GetRegisteredParticipantsCountAsync(currentEvent.Id);
                var dashboardDto = new DashboardDto
                {
                    EventName = currentEvent.Name,
                    TotalParticipants = totalParticipants,
                    RegisteredParticipants = registeredParticipants
                };
                _logger.LogInformation("{Prefix} Successfully retrieved dashboard summary", Prefix(methodName));
                return ResultModel<DashboardDto>.Ok(dashboardDto, "Dashboard summary retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Prefix} An error occurred while retrieving dashboard summary", Prefix(methodName));
                return ResultModel<DashboardDto>.Fail("An error occurred while retrieving dashboard summary", 500);
            }
        }
    }
}
