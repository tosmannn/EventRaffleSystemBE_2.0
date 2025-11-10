using Asp.Versioning;
using EventRaffle.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRaffle.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DashboardController : BaseController
    {
        private readonly IDashbaordService _dashboardService;
        public DashboardController(IDashbaordService dashbaordService)
        {
            _dashboardService = dashbaordService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetDashBoardSummary()
        {
            try
            {
                var result = await _dashboardService.GetDashboardSummary();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
