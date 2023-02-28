using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilsatIMS.Interfaces;

namespace MilsatIMS.Controllers
{
    [Route("api/prompts/")]
    [ApiController]
    public class PromptsController : ControllerBase
    {
        public PromptsController()
        {
        }

        /// <summary>
        /// Get all live prompts
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult> GetPrompts()
        {
            return Ok();
        }

        /// <summary>
        /// Add a new live prompt
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult> AddPrompt()
        {
            return Ok();
        }

        /// <summary>
        /// Delete a live prompt
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<ActionResult> DeletePrompt()
        {
            return Ok();
        }
    }


    [Route("api/activities/")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        public ActivitiesController()
        {
        }

        /// <summary>
        /// Get all latest activities
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult> GetActivities()
        {
            return Ok();
        }
    }
}
