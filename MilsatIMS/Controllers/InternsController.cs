using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilsatIMS.Data;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MilsatIMS.ViewModels.Interns;
using MilsatIMS.Models;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MilsatIMS.Controllers
{
    [Route("api/interns/")]
    [ApiController]
    public class InternsController : ControllerBase
    {
        private readonly IInternService _internService;
        public InternsController(IInternService service)
        {
            _internService = service;
        }


        /// <summary>
        /// Get all the interns in the system
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet, Authorize]
        public async Task<ActionResult<List<InternResponseDTO>>> GetIntern(Guid? sessionid, int pageNumber = 1, int pageSize = 15)
        {
            var result = await _internService.GetAllInterns(sessionid, pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Search for an intern in the system using id, name or team
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sessionid"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("search"), Authorize]
        public async Task<ActionResult<List<InternResponseDTO>>> FilterIntern(
            [FromQuery] GetInternVm model, Guid? sessionid,
            int pageNumber = 1, int pageSize = 15)
        {
            var result = await _internService.FilterInterns(model, sessionid, pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get an intern by id
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<List<InternResponseDTO>>> GetIntern(Guid? sessionid, Guid id)
        {
            var result = await _internService.GetInternById(sessionid, id);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        /// <summary>
        /// Modify the details of an intern by id (admin)
        /// </summary>
        /// <param name="intern"></param>
        /// <returns></returns>
        [HttpPut("modify"), Authorize(Roles = nameof(RoleType.Admin))]
        public async Task<ActionResult<InternResponseDTO>> PutIntern(UpdateInternVm intern)
        {
            var result = await _internService.UpdateIntern(intern);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}