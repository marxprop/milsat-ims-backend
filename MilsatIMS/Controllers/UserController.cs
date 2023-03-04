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
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet, Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> GetUsers(int pageNumber = 1, int pageSize = 15)
        {
            var result = await _userService.GetAllUsers(pageNumber, pageSize);
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
        public async Task<ActionResult<List<UserResponseDTO>>> GetUser(Guid id)
        {
            var result = await _userService.GetUserById(id);
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
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("search"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> FilterUsers([FromQuery] GetUserVm vm, int pageNumber=1, int pageSize=15)
        {
            var result = await _userService.FilterUsers(vm, pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        /// <summary>
        /// Update user's profile information
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPut("modify"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> UpdateUserProfile([FromForm] UpdateUserVm vm)
        {
            var result = await _userService.UpdateProfile(vm);
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
