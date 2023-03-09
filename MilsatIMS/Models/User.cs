using MilsatIMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Bio { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        [Required]
        public GenderType Gender { get; set; }
        [Required]
        public TeamType Team { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; } = new byte[32];
        [Required]
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? RefreshToken { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime PasswordTokenExpires { get; set; }
        [Required]
        public RoleType Role { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public bool isDeleted { get; set; } = false;
        public Intern Intern { get; set; }
        public Mentor Mentor { get; set; }
    }
}
