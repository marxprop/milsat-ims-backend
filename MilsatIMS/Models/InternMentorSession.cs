namespace MilsatIMS.Models
{
    public class InternMentorSession
    {
        public Guid InternId { get; set; }
        public Intern Intern { get; set; }
        public Guid MentorId { get; set; }
        public Mentor Mentor { get; set; }
        public Guid SessionId { get; set; }
        public Session Session { get; set; }
    }
}
