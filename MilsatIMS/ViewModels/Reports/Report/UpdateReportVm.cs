using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.ViewModels.Reports.Report
{
    public class UpdateReportVm
    {
        [Required(ErrorMessage = "Due date is required.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$", ErrorMessage = "Invalid due date format. Format should be yyyy-MM-dd HH:mm:ss")]
        public string DueDate { get; set; }
    }
}
