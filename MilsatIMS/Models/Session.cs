using MilsatIMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.Models
{
    public class Session
    {
        [Key]
        public Guid SessionId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Status Status { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime?  EndDate { get; set; }
        public List<InternMentorSession> IMS { get; set; }

    }
}
