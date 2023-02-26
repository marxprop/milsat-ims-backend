using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilsatInternAPI.Data;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MilsatInternAPI.ViewModels.Interns;
using MilsatInternAPI.Models;
using MilsatInternAPI.Enums;
using MilsatInternAPI.Interfaces;
using MilsatInternAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MilsatInternAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class InternsController : ControllerBase
    {
        private readonly IInternService _internService;
        public InternsController(IInternService service)
        {
            _internService = service;
        }


        // GET: api/Interns
        [HttpGet("GetAllInterns"), Authorize]
        public async Task<ActionResult<List<InternResponseDTO>>> GetIntern(int pageNumber = 1, int pageSize = 15)
        {
            var result = await _internService.GetAllInterns(pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("GetIntern"), Authorize]
        public async Task<ActionResult<List<InternResponseDTO>>> FilterIntern(
            [FromQuery] GetInternVm model,
            int pageNumber = 1, int pageSize = 15)
        {
            var result = await _internService.FilterInterns(model, pageNumber, pageSize);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetIntern/{id}"), Authorize]
        public async Task<ActionResult<List<InternResponseDTO>>> GetIntern(Guid id)
        {
            var result = await _internService.GetInternById(id);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        // PUT: api/Interns/5
        [HttpPut("UpdateIntern"), Authorize(Roles = nameof(RoleType.Admin))]
        public async Task<ActionResult<InternResponseDTO>> PutIntern(UpdateInternVm intern)
        {
            var result = await _internService.UpdateIntern(intern);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        // DELETE: api/Interns/5
        //[HttpDelete("RemoveIntern"), Authorize(Roles = "Admin")]
        //public async Task<IActionResult> DeleteIntern(Guid id)
        //{
        //    var result = await _internService.RemoveIntern(id);
        //    if (!result.Successful)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}
    }
}