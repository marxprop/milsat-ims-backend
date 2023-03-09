using MilsatIMS.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilsatIMS.Models
{
    public class Mentor
    {
        [Key]
        public Guid MentorId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public MentorStatus Status { get; set; }
        public List<InternMentorSession> IMS { get; set; }
        //public List<Intern> Interns { get; set; }
        //public List<Session> Sessions { get; set; }
    }
}
