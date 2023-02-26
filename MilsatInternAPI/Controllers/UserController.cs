using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilsatInternAPI.Interfaces;
using MilsatInternAPI.ViewModels.Users;

namespace MilsatInternAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Interns
        [HttpGet("GetAllUsers"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> GetUsers(int pageNumber = 1, int pageSize = 15)
        {
            var result = await _userService.GetAllUsers(pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetUser/{id}"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> GetUser(Guid id)
        {
            var result = await _userService.GetUserById(id);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result); 
        }

        [HttpGet("GetUsers"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> FilterUsers([FromQuery] GetUserVm vm, int pageNumber=1, int pageSize=15)
        {
            var result = await _userService.FilterUsers(vm, pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateUser"), Authorize]
        public async Task<ActionResult<List<UserResponseDTO>>> UpdateUserProfile([FromForm] UpdateUserVm vm)
        {
            var result = await _userService.UpdateProfile(vm);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("DeleteUser/{id}"), Authorize]
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
