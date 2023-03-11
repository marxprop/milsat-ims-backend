using MilsatIMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.Models
{
    public class Report
    {
        [Key]
        public Guid ReportId { get; set; }
        [Required]
        public string ReportName { get; set; }
        public DateTime CreatedDate {get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        [Required]
        public Guid SessionId { get; set; }
        public Session Session { get; set; }
        public List<ReportSubmission> Submissions { get; set; }
    }

    public class ReportSubmission
    {
        [Key]
        public Guid ReportSubmissionId { get; set; }
        [Required]
        public Guid ReportId { get; set; }
        public Report Report { get; set; }
        [Required]
        public Guid InternId { get; set; }
        public Intern Intern { get; set; }
        [Required]
        public ReportStatus Status { get; set; }
        [Required]
        public BlockerType BlockerType { get; set; } = BlockerType.None;
        [Required]
        public BlockerOrigin BlockerOrigin { get; set; } = BlockerOrigin.None;
        [Required]
        public string Task { get; set; } = string.Empty;
        [Required]
        public string TaskDetails { get; set; } = string.Empty;
        [Required]
        public Timeline Timeline { get; set; } = Timeline.None;
        [Required]
        public string OtherTeams { get; set; } = string.Empty ;
        public DateTime? SubmitDate { get; set; }
        [Required]
        public MentorRating MentorRating { get; set; } = MentorRating.None;
        public ReportFeedback? ReportFeedback { get; set; }
    }

    public class ReportFeedback
    {
        [Key]
        public Guid ReportFeedbackId { get; set; }
        public string Comment { get; set; }
        public InternRating InternRating { get; set; }
        public DateTime SubmitDate { get; set; }
        public Guid ReportSubmissionId { get; set; }
        public ReportSubmission ReportSubmission { get; set; }
        public Guid MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}
