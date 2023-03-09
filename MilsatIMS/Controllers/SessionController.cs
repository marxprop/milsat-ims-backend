using Microsoft.AspNetCore.Mvc;
using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Sessions;

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
        /// Create a new session
        /// </summary>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<GenericResponse<SessionDTO>>> CreateSession(SessionVm session)
        {
            var result = await _sessionService.CreateSession(session);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get the current live/ongoing session.
        /// </summary>
        /// <returns></returns>
        [HttpGet("live")]
        public async Task<ActionResult<GenericResponse<SessionDTO>>> GetCurrentSession()
        {
            var result = await _sessionService.GetCurrentSession();
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get all sessions
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<GenericResponse<SessionDTO>>> GetAllSession()
        {
            var result = await _sessionService.GetSessions();
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get a session by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse<SessionDTO>>> GeSessionById(Guid id)
        {
            var result = await _sessionService.GetSessionById(id);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// End the current live session
        /// </summary>
        /// <returns></returns>
        [HttpGet("end")]
        public async Task<ActionResult<GenericResponse<SessionDTO>>> CloseSession()
        {
            var result = await _sessionService.CloseSession();
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
