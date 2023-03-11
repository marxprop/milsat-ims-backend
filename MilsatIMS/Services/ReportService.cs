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
                    Message = "There is no ongoing session or sessionId does not exist"
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
                Successful = false,
                ResponseCode = ResponseCode.NotFound,
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

                if (!DateTime.TryParseExact(vm.DueDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDueDate))
                {
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "DateTime Format is wrong"
                    };
                }

                var newWeek = await GetNewReportWeekName();

                if (vm.ReportName != newWeek.Data)
                {
                    return new GenericResponse<ReportResponseDTO>
                    {
                        Successful = true,
                        ResponseCode = ResponseCode.Successful,
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

                var report = new Report { ReportId = reportId, ReportName = vm.ReportName, DueDate = parsedDueDate, SessionId = (Guid)_sessionId};

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

                return new GenericResponse<ReportResponseDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Report has been successfully created"
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

        //public async Task<GenericResponse<List<ReportResponseDTO>>> GetAllReports(Guid? sessionid)
        //{
        //    try
        //    {
        //        var _sessionId = await CheckSession(sessionid);
        //        if (_sessionId == null)
        //            return new GenericResponse<List<ReportResponseDTO>>
        //            {
        //                Successful = false,
        //                ResponseCode = ResponseCode.NotFound,
        //                Message = "There is no ongoing session or sessionId does not exist"
        //            };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error occured while Creating Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
        //        return new GenericResponse<List<ReportResponseDTO>>
        //        {
        //            Successful = false,
        //            ResponseCode = ResponseCode.EXCEPTION_ERROR,
        //            Message = "Error occured while creating intern"
        //        };
        //    }
        //}

        //public async Task<GenericResponse<ReportResponseDTO>> GetReportById(Guid? sessionid, Guid id)
        //{
        //    try
        //    {
        //        var _sessionId = await CheckSession(sessionid);
        //        if (_sessionId == null)
        //            return new GenericResponse<ReportResponseDTO>
        //            {
        //                Successful = false,
        //                ResponseCode = ResponseCode.NotFound,
        //                Message = "There is no ongoing session or sessionId does not exist"
        //            };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error occured while Creating Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
        //        return new GenericResponse<ReportResponseDTO>
        //        {
        //            Successful = false,
        //            ResponseCode = ResponseCode.EXCEPTION_ERROR,
        //            Message = "Error occured while creating intern"
        //        };
        //    }
        //}

        //public async Task<GenericResponse<ReportResponseDTO>> UpdateReport(Guid? sessionid, UpdateReportVm vm)
        //{
        //    try
        //    {
        //        var _sessionId = await CheckSession(sessionid);
        //        if (_sessionId == null)
        //            return new GenericResponse<ReportResponseDTO>
        //            {
        //                Successful = false,
        //                ResponseCode = ResponseCode.NotFound,
        //                Message = "There is no ongoing session or sessionId does not exist"
        //            };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error occured while Creating Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
        //        return new GenericResponse<ReportResponseDTO>
        //        {
        //            Successful = false,
        //            ResponseCode = ResponseCode.EXCEPTION_ERROR,
        //            Message = "Error occured while creating intern"
        //        };
        //    }
        //}

        public Task<GenericResponse<ReportResponseDTO>> GetReportById(Guid? sessionid, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<List<ReportResponseDTO>>> GetAllReports(Guid? sessionid)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<ReportResponseDTO>> UpdateReport(Guid? sessionid, UpdateReportVm vm)
        {
            throw new NotImplementedException();
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