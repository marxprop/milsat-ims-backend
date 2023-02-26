using MilsatInternAPI.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MilsatInternAPI.ViewModels.Interns
{
    public class CreateInternDTO
    {
        [Required]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter Valid Email ID")]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public GenderType Gender { get; set; }
        [Required]
        public string CourseOfStudy { get; set; }
        [Required]
        public string Institution { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public TeamType Team { get; set; }
    }
}
