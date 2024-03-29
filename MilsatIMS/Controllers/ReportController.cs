﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Reports.Report;
using Swashbuckle.AspNetCore.Annotations;

namespace MilsatIMS.Controllers
{
    [Route("api/reports/")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Create a new report (admin)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/reports/create
        ///     {
        ///         "reportName": "Week 1",
        ///         "dueDate": "2023-03-11 12:15:00"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<GenericResponse<ReportResponseDTO>>> CreateReport(CreateReportVm vm)
        {
            var result = await _reportService.CreateReport(vm);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Update the latest report (admin)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/reports/update
        ///     {
        ///         "dueDate": "2023-03-11 12:15:00"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult<GenericResponse<ReportResponseDTO>>> UpdateReport(UpdateReportVm vm)
        {
            var result = await _reportService.UpdateReport(vm);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get all created reports for the session
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<GenericResponse<List<ReportResponseDTO>>>> GetAllReports(Guid? sessionid)
        {
            var result = await _reportService.GetAllReports(sessionid);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get suitable new week name
        /// </summary>
        /// <returns></returns>
        [HttpGet("weekname")]
        public async Task<ActionResult<GenericResponse<ReportResponseDTO>>> GetNewReportWeekName()
        {
            var result = await _reportService.GetNewReportWeekName();
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get a report by id
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse<ReportResponseDTO>>> GetReportById(Guid? sessionid, Guid id)
        {
            var result = await _reportService.GetReportById(sessionid, id);
            if (!result.Successful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Submit an intern's report
        /// </summary>
        /// <returns></returns>
        [HttpPost("submit")]
        public async Task<ActionResult> SubmitReport()
        {
            return Ok();
        }

        /// <summary>
        /// Submit feedback from a mentor
        /// </summary>
        /// <returns></returns>
        [HttpPost("feedback")]
        public async Task<ActionResult> SubmitFeedback()
        {
            return Ok();
        }
    }
}
