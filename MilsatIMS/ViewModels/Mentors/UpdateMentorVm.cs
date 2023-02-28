using MilsatIMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.ViewModels.Mentors
{
    public class UpdateMentorVm
    {
        [Required]
        public Guid MentorId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public TeamType Team { get; set; }
    }
}
