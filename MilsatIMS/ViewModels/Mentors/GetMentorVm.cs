using MilsatIMS.Enums;

namespace MilsatIMS.ViewModels.Mentors
{
    public class GetMentorVm
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public TeamType? Team { get; set; }
    }
}
