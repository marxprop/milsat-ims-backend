using MilsatIMS.Enums;

namespace MilsatIMS.ViewModels.Sessions
{
    public class SessionVm
    {
        public Guid SessionId { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}
