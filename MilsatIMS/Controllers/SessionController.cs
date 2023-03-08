using Microsoft.AspNetCore.Mvc;
using MilsatIMS.Interfaces;

namespace MilsatIMS.Controllers
{
    [Route("api/sessions/")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// Add a new session
        /// </summary>
        /// <returns></returns>
        [HttpPost("submit")]
        public async Task<ActionResult> SubmitReport()
        {
            return Ok();
        }
    }
}
