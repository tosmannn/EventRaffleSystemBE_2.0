using Asp.Versioning;
using EventRaffle.Core.DTOs.Event;
using EventRaffle.Core.Interfaces.Services;
using EventRaffle.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventRaffle.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EventController : BaseController
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateEventDto dto)
        {
            try
            {
                var result = await _eventService.CreateEventAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultModel<EventDto>
                        .Fail("An unexpected error occurred.", 500, new List<string> { ex.Message }));
            }
        }

        [HttpGet("validate-name")]
        public async Task<IActionResult> ValidateEventName([FromQuery] string name)
        {
            try
            {
                var result = await _eventService.ValidateEventNameAsync(name);

                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultModel<EventDto>
                        .Fail("An unexpected error occurred.", 500, new List<string> { ex.Message }));
            }
        }

        [HttpGet("active-event")]
        public async Task<IActionResult> GetActiveEventId()
        {
            try
            {
                var result = await _eventService.GetActiveEventNameAndIdAsync();

                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultModel<EventDto>
                        .Fail("An unexpected error occurred.", 500, new List<string> { ex.Message }));
            }
        }
    }
}
