using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.ViewModels.Reports.Report
{
    public class CreateReportVm
    {
        [Required(ErrorMessage = "Report name is required.")]
        [RegularExpression(@"^Week\s\d+$", ErrorMessage = "Invalid report name format. Format should be 'Week [number]'.")]
        public string ReportName { get; set; }

        [Required(ErrorMessage = "Due date is required.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$", ErrorMessage = "Invalid due date format. Format should be yyyy-MM-dd HH:mm:ss")]
        public string DueDate { get; set; }
    }
}
