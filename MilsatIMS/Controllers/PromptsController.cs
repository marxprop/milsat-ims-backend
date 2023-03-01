using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Prompts;

namespace MilsatIMS.Controllers
{
    [Route("api/prompts/")]
    [ApiController]
    public class PromptsController : ControllerBase
    {
        private readonly IPromptService _promptService;
        public PromptsController(IPromptService promptService)
        {
            _promptService = promptService;
        }

        /// <summary>
        /// Get the list of all live prompts
        /// </summary>
        /// <returns></returns>
        [HttpGet(), Authorize]
        public async Task<ActionResult<GenericResponse<List<PromptDTO>>>> GetPrompts()
        {
            var result = await _promptService.GetPrompts();
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Add a new live prompt
        /// </summary>
        /// <returns></returns>
        [HttpPost("add"), Authorize(Roles = nameof(RoleType.Admin))]
        public async Task<ActionResult<GenericResponse<PromptDTO>>> AddPrompt(PromptVm prompt)
        {
            var result = await _promptService.AddPrompt(prompt);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete a live prompt
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete/{id}"), Authorize(Roles = nameof(RoleType.Admin))]
        public async Task<ActionResult<GenericResponse<PromptDTO>>> DeletePrompt(Guid id)
        {
            var result = await _promptService.DeletePrompt(id);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
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
