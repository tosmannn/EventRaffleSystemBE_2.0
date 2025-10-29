using AutoMapper;
using EventRaffle.Core.DTOs.Participant;
using EventRaffle.Core.Entities;
using EventRaffle.Core.Interfaces.Repositories;
using EventRaffle.Core.Interfaces.Services;
using EventRaffle.Core.Models;
using ExcelDataReader;
using Microsoft.Extensions.Logging;

namespace EventRaffle.Service.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly ILogger<ParticipantService> _logger;
        private readonly IMapper _mapper;

        private readonly IParticipantRepository _participantRepository;

        private const string LogPrefix = "[ParticipantService]";
        private string Prefix(string methodName) => $"{LogPrefix} [{methodName}]";

        public ParticipantService(ILogger<ParticipantService> logger
            , IMapper mapper
            , IParticipantRepository participantRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _participantRepository = participantRepository;
        }

        public async Task<ResultModel<int>> UploadParticipantsAsync(Stream file, Guid eventId)
        {
            var methodName = nameof(UploadParticipantsAsync);

            _logger.LogInformation("{Prefix} Uploading participants for EventId: {EventId}", Prefix(methodName), eventId);

            try
            {
                var participants = ParseAsync(file, eventId);

                await _participantRepository.AddListAsync(participants);
                await _participantRepository.SaveChangesAsync();

                _logger.LogInformation("{Prefix} Successfully uploaded {Count} participants for EventId: {EventId}", Prefix(methodName), participants.Count, eventId);
                return ResultModel<int>.Ok(participants.Count, "Participants uploaded successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Prefix} Error uploading participants for EventId: {EventId}", Prefix(methodName), eventId);
                return ResultModel<int>.Fail("An error occurred while uploading participants.", 500, new List<string> { ex.Message });
            } 
            finally
            {
                _logger.LogInformation("{Prefix} Finished uploading participants for EventId: {EventId}", Prefix(methodName), eventId);
            }
        }


        public async Task<ResultModel<IEnumerable<ParticipantDto>>> GetAllAsync(Guid eventId)
        {
            var methodName = nameof(GetAllAsync);

            _logger.LogInformation("{Prefix} Get all participants for EventId: {EventId}", Prefix(methodName), eventId);

            try
            {
                var participants = await _participantRepository.GetAllAsync(eventId);

                if (!participants.Any())
                {
                    return ResultModel<IEnumerable<ParticipantDto>>.Ok(new List<ParticipantDto>(), "No participants found.");
                }

                var participantDto = _mapper.Map<IEnumerable<ParticipantDto>>(participants);

               _logger.LogInformation("{Prefix} Successfully pulled {Count} participants for EventId: {EventId}", Prefix(methodName), participantDto.Count(), eventId);

                return ResultModel<IEnumerable<ParticipantDto>>.Ok(participantDto, "Successfully pulled participants");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Prefix} Error getting participants for EventId: {EventId}", Prefix(methodName), eventId);

                return ResultModel<IEnumerable<ParticipantDto>>.Fail("An error occurred while getting participants.", 500, new List<string> { ex.Message });
            }
            finally
            {
                _logger.LogInformation("{Prefix} Finished getting participants for EventId: {EventId}", Prefix(methodName), eventId);
            }
        }


        #region Private Methods
        private List<Participant> ParseAsync(Stream stream, Guid eventId)
        {
            using var reader = ExcelReaderFactory.CreateReader(stream);
            var result = reader.AsDataSet();

            var table = result.Tables[0]; // Assuming the first sheet contains the participant data
            var participants = new List<Participant>();

            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                var firstName = row[0]?.ToString()?.Trim() ?? "";
                var lastName = row[1]?.ToString()?.Trim() ?? "";
                var employeeId = row[2]?.ToString()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName)) continue;

                participants.Add(new Participant
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmployeeId = string.IsNullOrWhiteSpace(employeeId) ? null : employeeId,
                    EventId = eventId,
                    IsRegistered = false,
                });

            }

            return participants;
        }
        #endregion

    }
}
