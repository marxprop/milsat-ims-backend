using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels.Stats;

namespace MilsatIMS.Controllers
{
    [Route("api/stats/")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        public readonly IStatsService _statsService;

        public StatsController (IStatsService statsService)
        {
            _statsService = statsService;
        }

        /// <summary>
        /// Get the total number of interns and mentors
        /// </summary>
        /// <returns></returns>
        [HttpGet("total")]
        public async Task<ActionResult<GetTotalUsersDTO>> GetTotalUsers()
        {
            var result = await _statsService.GetTotalUsers();
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get the number of interns in each team
        /// </summary>
        /// <returns></returns>
        [HttpGet("teams-count")]
        public async Task<ActionResult<GetTeamTotalDTO>> GetTeamTotal()
        {
            var result = await _statsService.GetTeamTotal();
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
