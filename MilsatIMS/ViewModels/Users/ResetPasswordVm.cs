using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.ViewModels.Users
{
    public class ResetPasswordVm
    {
        [Required]
        public string Token { get; set; }
        [Required, MinLength(9)]
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
