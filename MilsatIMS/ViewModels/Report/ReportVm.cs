using MilsatIMS.Enums;

namespace MilsatIMS.ViewModels.Report
{
    public class ReportVm
    {
        public BlockerType BlockerType { get; set; }
        public BlockerOrigin BlockerOrigin { get; set; }
        public string Task { get; set; }
        public string TaskDetails { get; set; }
        public Timeline Timeline { get; set; }
        public List<TeamType> OtherTeam { get; set; }
    }
}
