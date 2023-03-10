﻿using MilsatIMS.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilsatIMS.Models
{
    public class Intern
    {
        [Key]
        public Guid InternId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string CourseOfStudy { get; set; }
        public string Institution { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int Year { get; set; } = DateTime.UtcNow.Year;
        public List<InternMentorSession> IMS { get; set; }

        //public Guid? MentorId { get; set; }
        //[ForeignKey("MentorId")]
        //public Mentor? Mentor { get; set; }
        //public List<Session> Sessions { get; set; }
    }
}
