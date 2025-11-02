using Asp.Versioning;
using EventRaffle.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRaffle.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RaffleController : BaseController
    {
        private readonly IRaffleService _raffleService;
        public RaffleController(IRaffleService raffleService)
        {
            _raffleService = raffleService;
        }

        [HttpGet("draw/{eventId}")]
        public async Task<IActionResult> DrawWinnerAsync(Guid eventId)
        {
            try
            {
                var result = await _raffleService.DrawWinnerAsync(eventId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
