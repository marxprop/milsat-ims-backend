using System.ComponentModel.DataAnnotations;

namespace MilsatInternAPI.ViewModels.Users
{
    public class ForgetPasswordVm
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
