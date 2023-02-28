using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.ViewModels
{
    public class UserLoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(9)]
        public string Password { get; set; }

    }
}
