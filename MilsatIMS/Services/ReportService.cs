using Hangfire;
using Microsoft.EntityFrameworkCore;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.Models;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Reports.Report;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MilsatIMS.Services
{
    public class ReportService : IReportService
    {
        private readonly ILogger<ReportService> _logger;
        private readonly IAsyncRepository<Session> _sessionRepo;
        private readonly IAsyncRepository<Report> _reportRepo;
        private readonly IAsyncRepository<Intern> _internRepo;
        private readonly IAsyncRepository<ReportSubmission> _reportSubRepo;

        public ReportService(IAsyncRepository<Session> sessionRepo, ILogger<ReportService> logger, IAsyncRepository<Report> reportRepo,
            IAsyncRepository<Intern> internRepo, IAsyncRepository<ReportSubmission> reportSubRepo)
        {
            _logger = logger;
            _sessionRepo = sessionRepo;
            _reportRepo = reportRepo;
            _internRepo = internRepo;
            _reportSubRepo = reportSubRepo;
        }

        public async Task<Guid?> CheckSession(Guid? sessionId)
        {
            if (sessionId == null)
            {
                var _currentSession = await _sessionRepo.GetAll().Where(x => x.Status == Status.Current).SingleOrDefaultAsync();
                var id = _currentSession?.SessionId;
                return id;
            }
            else
            {
                var _currentSession = await _sessionRepo.GetAll().Where(x => x.SessionId == sessionId).SingleOrDefaultAsync();
                var id = _currentSession?.SessionId;
                return id;
            }
        }

        public async Task<GenericResponse<string>> GetNewReportWeekName()
        {

            var _sessionId = await CheckSession(null);
            if (_sessionId == null)
                return new GenericResponse<string>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.NotFound,
                    Message = "There is no ongoing session"
                };

            var latestReport = await _reportRepo.GetAll().Where(x => x.SessionId == _sessionId)
                    .OrderByDescending(r => r.CreatedDate)
                    .SingleOrDefaultAsync();

            var latestReportName = latestReport?.ReportName;
            if (latestReportName == null)
            {
                latestReportName = "Week 0";
            }

            var latestWeekNumber = int.Parse(Regex.Match(latestReportName, @"\d+").Value);
            var newWeekName = $"Week {latestWeekNumber + 1}";
            return new GenericResponse<string>
            {
                Successful = true,
                ResponseCode = ResponseCode.Successful,
                Message = "Successfully provided the right new week name",
                Data = newWeekName
            };
        }

        public async Task<GenericResponse<ReportResponseDTO>> CreateReport(CreateReportVm vm)
        {
            try
            {
                var _sessionId = await CheckSession(null);
                if (_sessionId == null)
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                bool liveReportExists = await _reportRepo.AnyAsync(x => x.Status == Status.Current);
                if (liveReportExists)
                {
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "You can not create a new report when there is a current live one."
                    };
                }

                if (!DateTime.TryParseExact(vm.DueDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDueDate))
                {
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "DateTime Format is wrong"
                    };
                }

                var report = new Report { DueDate = parsedDueDate, SessionId = (Guid)_sessionId, Status = Status.Current };

                if (parsedDueDate <= report.CreatedDate)
                {
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "The due date should be later than the time you are creating the report"
                    };
                }

                var newWeek = await GetNewReportWeekName();

                if (vm.ReportName != newWeek.Data)
                {
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "The report name is not right. Ensure you are setting the right next report."
                    };
                }


                Guid reportId = Guid.NewGuid();
                bool guidExists = await _reportRepo.AnyAsync(x => x.ReportId == reportId);
                while (guidExists)
                {
                    reportId = Guid.NewGuid();
                    guidExists = await _reportRepo.AnyAsync(x => x.ReportId == reportId);
                }

                report.ReportId = reportId;
                report.ReportName = vm.ReportName;
                var nullSubmissions = new List<ReportSubmission>();

                var interns = await _internRepo.GetAll()
                                               .Include(x => x.IMS.Where(ims => ims.SessionId == _sessionId))
                                               .Select(i => new
                                               {
                                                   InternId = i.InternId,
                                               })
                                               .ToListAsync();
                foreach (var intern in interns)
                {
                    var submission = new ReportSubmission
                    {
                        InternId = intern.InternId,
                        ReportId = report.ReportId,
                        Status = ReportStatus.Undone,
                    };
                    nullSubmissions.Add(submission);
                }
                await _reportRepo.AddAsync(report);
                await _reportSubRepo.AddRangeAsync(nullSubmissions);
                var reportdto = new ReportResponseDTO
                {
                    ReportId = report.ReportId,
                    ReportName = report.ReportName,
                    DueDate = vm.DueDate,
                };

                var jobId = BackgroundJob.Schedule(() => UpdateAllLiveReportStatus(report.ReportId), parsedDueDate);

                return new GenericResponse<ReportResponseDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Report has been successfully created",
                    Data = reportdto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while creating Report. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<ReportResponseDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while creating report"
                };
            }
        }

        public async Task<GenericResponse<List<ReportResponseDTO>>> GetAllReports(Guid? sessionid)
        {
            try
            {
                var _sessionId = await CheckSession(sessionid);
                if (_sessionId == null)
                    return new GenericResponse<List<ReportResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                var reports = await _reportRepo.GetAll().Where(x => x.SessionId == _sessionId).ToListAsync();

                var reportdtos = new List<ReportResponseDTO>();
                foreach (Report report in reports)
                {
                    var r = new ReportResponseDTO
                    {
                        ReportId = report.ReportId,
                        ReportName = report.ReportName,
                        DueDate = report.DueDate.ToString("dd-MMMM-yy")
                    };
                    reportdtos.Add(r);
                }
                return new GenericResponse<List<ReportResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Successfully fetched all created reports",
                    Data = reportdtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while getting all reports. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<ReportResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while fetching all reports"
                };
            }
        }

        public async Task<GenericResponse<ReportResponseDTO>> GetReportById(Guid? sessionid, Guid id)
        {
            try
            {
                var _sessionId = await CheckSession(sessionid);
                if (_sessionId == null)
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                var report = await _reportRepo.GetAll().Where(x => x.ReportId == id && x.SessionId == _sessionId).FirstOrDefaultAsync();
                if (report == null)
                {
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "No report with this id was found"
                    };
                }

                var reportdto = new ReportResponseDTO
                {
                    ReportId = report.ReportId,
                    ReportName = report.ReportName,
                    DueDate = report.DueDate.ToString("dd-MMMM-yy"),
                };
                return new GenericResponse<ReportResponseDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Report has been successfully updated",
                    Data = reportdto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while Creating Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<ReportResponseDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while creating intern"
                };
            }
        }

        public async Task<GenericResponse<ReportResponseDTO>> UpdateReport(UpdateReportVm vm)
        {
            try
            {
                var _sessionId = await CheckSession(null);
                if (_sessionId == null)
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                var report = await _reportRepo.GetAll().Where(x => x.Status == Status.Current && x.SessionId == _sessionId).FirstOrDefaultAsync();
                if (report == null)
                {
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing live report to update"
                    };
                }

                if(!DateTime.TryParseExact(vm.DueDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDueDate))
                {
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "DateTime Format is wrong"
                    };
                }

                if (parsedDueDate <= DateTime.UtcNow)
                {
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "The due date should be later than the time you are updating the report"
                    };
                }

                report.DueDate = parsedDueDate;
                await _reportRepo.UpdateAsync(report);
                var reportdto = new ReportResponseDTO
                {
                    ReportId = report.ReportId,
                    ReportName = report.ReportName,
                    DueDate = vm.DueDate,
                };
                return new GenericResponse<ReportResponseDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Report has been successfully updated",
                    Data = reportdto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while Creating Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<ReportResponseDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while creating intern"
                };
            }
        }

        public async Task UpdateAllLiveReportStatus(Guid reportId)
        {
            try
            {
                var report = await _reportRepo.GetAll().Where(x => x.ReportId == reportId && x.Status == Status.Current).FirstOrDefaultAsync();
                if (report != null)
                {
                    report.Status = Status.Complete;

                    List<ReportSubmission> submissions = await _reportSubRepo.GetAll().Where(x => x.ReportId == reportId).ToListAsync();
                    foreach (var submission in submissions)
                    {
                        if (submission.Status == ReportStatus.Undone)
                        {
                            submission.Status = ReportStatus.Overdue;
                        }
                    }

                    await _reportRepo.UpdateAsync(report);
                    await _reportSubRepo.UpdateRangeAsync(submissions);
                }
            }
            catch(Exception ex){
                _logger.LogError($"Error occured while updating scheduled  report status for interns. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
            }
        }
    }
}

//try
//{
//    var _sessionId = await CheckSession(sessionId);
    //if (_sessionId == null)
    //    return new GenericResponse<ReportResponseDTO>
    //    {
    //        Successful = false,
    //        ResponseCode = ResponseCode.NotFound,
    //        Message = "There is no ongoing session or sessionId does not exist"
    //    };
//}
//catch (Exception ex)
//{
//    _logger.LogError($"Error occured while Creating Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
//    return new GenericResponse<ReportResponseDTO>
//    {
//        Successful = false,
//        ResponseCode = ResponseCode.EXCEPTION_ERROR,
//        Message = "Error occured while creating intern"
//    };
//}