using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.ViewModels.Users
{
    public class ForgetPasswordVm
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
