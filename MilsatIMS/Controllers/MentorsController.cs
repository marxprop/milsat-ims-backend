using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilsatIMS.Data;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.Models;
using MilsatIMS.ViewModels.Mentors;


namespace MilsatIMS.Controllers
{
    [Route("api/mentors/")]
    [ApiController]
    public class MentorsController : ControllerBase
    {
        private readonly IMentorService _mentorService;

        public MentorsController(IMentorService service)
        {
            _mentorService = service;
        }

        /// <summary>
        /// Get all the mentors in the system
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet, Authorize]
        public async Task<ActionResult<List<MentorResponseDTO>>> GetMentor(int pageNumber = 1, int pageSize = 15)
        {
            var result = await _mentorService.GetAllMentors(pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Search for a mentor in the system using id, name or team
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("search"), Authorize] //(Roles = $"{nameof(RoleType.Admin)}, {nameof(RoleType.Mentor)}")
        public async Task<ActionResult<MentorResponseDTO>> GetMentor([FromQuery] GetMentorVm vm, int pageNumber = 1, int pageSize = 15)
        {
            var result = await _mentorService.GetMentors(vm, pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get a mentor by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<MentorResponseDTO>> GetMentor(Guid id)
        {
            var result = await _mentorService.GetMentorById(id);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Modify the details of a mentor by id (admin)
        /// </summary>
        /// <param name="mentor"></param>
        /// <returns></returns>
        [HttpPut("modify"), Authorize(Roles = nameof(RoleType.Admin))]
        public async Task<ActionResult<List<MentorResponseDTO>>> PutMentor(UpdateMentorVm mentor)
        {
            var result = await _mentorService.UpdateMentor(mentor);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
