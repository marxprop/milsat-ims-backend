using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels.Users;

namespace MilsatIMS.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all the users in the system
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet, Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> GetUsers(Guid? sessionid, int pageNumber = 1, int pageSize = 15)
        {
            var result = await _userService.GetAllUsers(sessionid, pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> GetUser(Guid? sessionid, Guid id)
        {
            var result = await _userService.GetUserById(sessionid, id);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result); 
        }


        /// <summary>
        /// Search for a user in the system using id, name or team
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="sessionid"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("search"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> FilterUsers([FromQuery] GetUserVm vm, Guid? sessionid, int pageNumber=1, int pageSize=15)
        {
            var result = await _userService.FilterUsers(vm,sessionid, pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        /// <summary>
        /// Update an intern's user profile information
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPut("intern/modify"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> UpdateInternProfile([FromForm] UpdateInternVm vm)
        {
            var result = await _userService.UpdateInternProfile(vm);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Update a mentor's user profile information
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPut("mentor/modify"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> UpdateMentorProfile([FromForm] UpdateMentorVm vm)
        {
            var result = await _userService.UpdateMentorProfile(vm);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Soft Delete a user from the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> DeleteUser(Guid id)
        {
            var result = await _userService.RemoveUser(id);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
