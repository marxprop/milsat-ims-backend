using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels;

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
        /// Submit an intern's report
        /// </summary>
        /// <returns></returns>
        [HttpPost("submit")]
        public async Task<ActionResult> SubmitReport()
        {
            return Ok();
        }
    }
}
