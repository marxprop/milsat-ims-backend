using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Interns;
using MilsatIMS.ViewModels.Mentors;
using MilsatIMS.ViewModels.Users;

namespace MilsatIMS.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthentication _authService;
        public AuthenticateController(IAuthentication authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticate a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(UserLoginDTO request)
        {
            var result = await _authService.Login(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Refresh a user's login token
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(RefreshTokenVm token)
        {
            var response = await _authService.RefreshToken(token);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        /// <summary>
        /// Send a redirection link to user's mail address for password reset
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost("forgot-password"), AllowAnonymous]
        public async Task<ActionResult<ForgotPasswordResponse>> ForgotPassword(ForgetPasswordVm vm)
        {
            var response = await _authService.ForgotPassword(vm);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        /// <summary>
        /// Reset a user's password
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost("reset-password"), AllowAnonymous]
        public async Task<ActionResult<ForgotPasswordResponse>> ResetPassword(ResetPasswordVm vm)
        {
            var response = await _authService.ResetPassword(vm);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }

    [Route("api/register/")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IInternService _internService;
        private readonly IMentorService _mentorService;
        private readonly IAuthentication _authService;
        public RegisterController(IInternService internService, IMentorService mentorService)
        {
            _internService = internService;
            _mentorService = mentorService;
        }


        /// <summary>
        /// Register a new intern
        /// </summary>
        /// <param name="intern"></param>
        /// <returns></returns>
        [HttpPost("intern"), Authorize(Roles = nameof(RoleType.Admin))]
        public async Task<ActionResult<InternResponseDTO>> RegisterIntern(CreateInternDTO intern)
        {
            var result = await _internService.AddIntern(intern);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Register a new mentor
        /// </summary>
        /// <param name="mentor"></param>
        /// <returns></returns>
        [HttpPost("mentor"), Authorize(Roles = nameof(RoleType.Admin))]
        public async Task<ActionResult<InternResponseDTO>> RegisterMentor(CreateMentorVm mentor)
        {
            var result = await _mentorService.AddMentor(mentor);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
