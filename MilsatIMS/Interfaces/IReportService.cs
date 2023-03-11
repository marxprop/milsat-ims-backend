using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Reports.Report;

namespace MilsatIMS.Interfaces
{
    public interface IReportService
    {
        Task<GenericResponse<string>> GetNewReportWeekName();
        Task<GenericResponse<ReportResponseDTO>> CreateReport(CreateReportVm vm);
        Task<GenericResponse<ReportResponseDTO>> GetReportById(Guid? sessionid, Guid id);
        Task<GenericResponse<List<ReportResponseDTO>>> GetAllReports(Guid? sessionid);
        Task<GenericResponse<ReportResponseDTO>> UpdateReport(Guid? sessionid, UpdateReportVm vm);
    }
}
