using Asp.Versioning;
using EventRaffle.Core.Interfaces.Services;
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

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
