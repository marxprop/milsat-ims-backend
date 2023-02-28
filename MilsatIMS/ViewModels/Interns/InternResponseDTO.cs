using MilsatIMS.Enums;

namespace MilsatIMS.ViewModels.Interns
{
    public class InternResponseDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public TeamType Team { get; set; }
        public string CourseOfStudy { get; set; }
        public string Institution { get; set; }
        public GenderType Gender { get; set; }
        public Guid? MentorUserId { get; set; } 
        public int Year { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
    }
}
    