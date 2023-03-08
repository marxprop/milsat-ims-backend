using MilsatIMS.Enums;

namespace MilsatIMS.ViewModels.Sessions
{
    public class SessionDTO
    {
        public Guid SessionId { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
