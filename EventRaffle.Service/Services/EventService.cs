using AutoMapper;
using EventRaffle.Core.DTOs.Event;
using EventRaffle.Core.Entities;
using EventRaffle.Core.Interfaces.Repositories;
using EventRaffle.Core.Interfaces.Services;
using EventRaffle.Core.Models;
using Microsoft.Extensions.Logging;

namespace EventRaffle.Service.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ILogger<EventService> _logger;
        private readonly IMapper _mapper;

        private const string LogPrefix = "[EventService]";
        private string Prefix(string methodName) => $"{LogPrefix} [{methodName}]";

        public EventService(IEventRepository eventRepository,
                            ILogger<EventService> logger,
                            IMapper mapper)
        {
            _eventRepository = eventRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResultModel<EventDto>> CreateEventAsync(CreateEventDto dto)
        {
            var methodName = nameof(CreateEventAsync);

            _logger.LogInformation("{Prefix} Creating new event", Prefix(methodName));

            try
            {
                if (dto.IsActive)
                    await SetEventsToInActiveAsync();

                var entity = _mapper.Map<Event>(dto);
                await _eventRepository.AddAsync(entity);
                await _eventRepository.SaveChangesAsync();

                var resultDto = _mapper.Map<EventDto>(entity);

                _logger.LogInformation("{Prefix} Event created successfully with ID: {EventId}", Prefix(methodName), entity.Id);

                return ResultModel<EventDto>.Ok(resultDto, "Event created successfully", 201);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Prefix} Error occurred while creating new event: {Message}", Prefix(methodName), ex.Message);
                return ResultModel<EventDto>.Fail("An error occurred while creating the event", 500);
            } 
            finally
            {
                _logger.LogInformation("{Prefix} Finished creating new event", Prefix(methodName));
            }
        }

        public async Task<ResultModel<bool>> ValidateEventNameAsync(string eventName)
        {
            var methodName = nameof(ValidateEventNameAsync);

            _logger.LogInformation("{Prefix} Validating event name: {EventName}", Prefix(methodName), eventName);

            try
            {
                var exists = await _eventRepository.EventNameExistAsync(eventName);

                _logger.LogInformation("{Prefix} Event name validation completed. Exists: {Exists}", Prefix(methodName), exists);

                return ResultModel<bool>.Ok(!exists, exists ? "Event name already exists" : "Event name is available", 200); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Prefix} Error occurred while validating event name: {Message}", Prefix(methodName), ex.Message);
                return ResultModel<bool>.Fail("An error occurred while validating the event name", 500);
            }
            finally
            {
                _logger.LogInformation("{Prefix} Finished validating event name", Prefix(methodName));
            }
        }

        public async Task<ResultModel<EventNameAndIdDto?>> GetActiveEventNameAndIdAsync()
        {
            var methodName = nameof(GetActiveEventNameAndIdAsync);
            _logger.LogInformation("{Prefix} Retrieving active event ID", Prefix(methodName));

            try
            {
                var activeEvent = await _eventRepository.GetActiveEventAsync();

                if (activeEvent == null)
                {
                    _logger.LogInformation("{Prefix} No active event found", Prefix(methodName));
                    return ResultModel<EventNameAndIdDto?>.Ok(null, "No active event found", 200);
                }

                var eventDto = _mapper.Map<EventNameAndIdDto>(activeEvent);

                _logger.LogInformation("{Prefix} Active event found with ID: {EventId}", Prefix(methodName), activeEvent.Id);

                return ResultModel<EventNameAndIdDto?>.Ok(eventDto, "Active event retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Prefix} Error occurred while retrieving active event ID: {Message}", Prefix(methodName), ex.Message);
                return ResultModel<EventNameAndIdDto?>.Fail("An error occurred while retrieving the active event ID", 500);
            }
            finally
            {
                _logger.LogInformation("{Prefix} Finished retrieving active event ID", Prefix(methodName));
            }
        }

        #region Private Methods
        private async Task SetEventsToInActiveAsync()
        {
            var events = await _eventRepository.GetAllActiveEventsAsync();

            foreach (var ev in events)
            {
                ev.IsActive = false;
                ev.ModifiedUtcDate = DateTime.UtcNow;
            }

            await _eventRepository.SaveChangesAsync();
        }
        #endregion

    }
}
