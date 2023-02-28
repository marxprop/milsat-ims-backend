using MilsatIMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.ViewModels.Mentors
{
    public class CreateMentorVm
    {
        [Required]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter Valid Email ID")]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public GenderType Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public TeamType Team { get; set; }
    }
}

