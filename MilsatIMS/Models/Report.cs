using MilsatIMS.Enums;

namespace MilsatIMS.Models
{
    public class Report
    {
        public Guid ReportId { get; set; }
        public Guid ReportName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime Deadline { get; set;}

    }
    public class ReportSubmission
    {
        public Guid ReportSubmissionId { get; set; }
        public BlockerType BlockerType { get; set; }
        public BlockerOrigin BlockerOrigin { get; set; }
        public string Task { get; set; }
        public string TaskDetails { get; set; }
        public Timeline Timeline { get; set; }
        public List<TeamType> OtherTeam { get; set; }
        public DateTime SubmitDate { get; set; }
        public MentorRating MentorRating { get; set; }
    }

    public class ReportFeedback
    {
        public Guid ReportFeedbackId { get; set; }
        public string Comment { get; set; }
        public InternRating InternRating { get; set; }
        public DateTime SubmitDate { get; set; }
    }
}
