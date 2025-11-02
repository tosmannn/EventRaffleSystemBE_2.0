using Asp.Versioning;
using EventRaffle.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRaffle.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ParticipantController :BaseController
    {
        private readonly IParticipantService _participantService;
        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadParticipants(IFormFile file, Guid eventId)
        {
            try
            {
                if(file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                if (eventId == Guid.Empty)
                    return BadRequest("Invalid event ID.");

                await using var stream = file.OpenReadStream();
                var result = await _participantService.UploadParticipantsAsync(stream, eventId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpGet("event/{eventId}/participants")]
        public async Task<IActionResult> Get(Guid eventId)
        {
            try
            {
                if (eventId == Guid.Empty)
                    return BadRequest("Invalid event ID.");

                var result = await _participantService.GetAllAsync(eventId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPost("{id}/register")]
        public async Task<IActionResult> RegisterAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("Invalid participantId");

                var result = await _participantService.RegisterAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
