using Microsoft.AspNetCore.Mvc;

namespace EventRaffle.API.Controllers
{
    public abstract class BaseController : Controller
    {
        // Common response helpers
        protected IActionResult OkResponse<T>(T data) => Ok(new { success = true, data });

        protected IActionResult ErrorResponse(string message) =>
            BadRequest(new { success = false, error = message });

    }
}
