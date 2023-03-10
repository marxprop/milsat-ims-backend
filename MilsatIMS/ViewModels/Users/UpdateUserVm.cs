using MilsatIMS.Enums;

namespace MilsatIMS.ViewModels.Users
{
    public class UpdateInternVm
    {
        public string CourseOfStudy { get; set; }
        public string Institution { get; set; } 
        public IFormFile ProfilePicture { get; set; }
        public string Bio { get; set; }
    }

    public class UpdateMentorVm
    {
        public IFormFile ProfilePicture { get; set; }
        public string Bio { get; set; }
    }
}
