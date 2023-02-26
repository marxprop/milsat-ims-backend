using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilsatInternAPI.Data;
using MilsatInternAPI.Enums;
using MilsatInternAPI.Interfaces;
using MilsatInternAPI.Models;
using MilsatInternAPI.ViewModels.Mentors;


namespace MilsatInternAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MentorsController : ControllerBase
    {
        private readonly IMentorService _mentorService;

        public MentorsController(IMentorService service)
        {
            _mentorService = service;
        }


        // GET: api/Mentors
        [HttpGet("GetAllMentors"), Authorize]
        public async Task<ActionResult<List<MentorResponseDTO>>> GetMentor(int pageNumber = 1, int pageSize = 15)
        {
            var result = await _mentorService.GetAllMentors(pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        // GET: api/Mentors/5
        [HttpGet("GetMentor"), Authorize] //(Roles = $"{nameof(RoleType.Admin)}, {nameof(RoleType.Mentor)}")
        public async Task<ActionResult<MentorResponseDTO>> GetMentor([FromQuery] GetMentorVm vm, int pageNumber = 1, int pageSize = 15)
        {
            var result = await _mentorService.GetMentors(vm, pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET: api/Mentors/5
        [HttpGet("GetMentor/{id}"), Authorize]
        public async Task<ActionResult<MentorResponseDTO>> GetMentor(Guid id)
        {
            var result = await _mentorService.GetMentorById(id);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT: api/Mentors/5
        [HttpPut("UpdateMentor"), Authorize(Roles = nameof(RoleType.Admin))]
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
