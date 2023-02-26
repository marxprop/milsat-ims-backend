using MilsatInternAPI.Enums;

namespace MilsatInternAPI.ViewModels.Users
{
    public class UserResponseDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public TeamType Team { get; set; }
        public RoleType Role { get; set; }
    }
}
