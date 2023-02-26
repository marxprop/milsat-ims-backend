using MilsatInternAPI.Enums;

namespace MilsatInternAPI.ViewModels.Users
{
    public class CreateUserVm
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public TeamType Team { get; set; }
        public RoleType Role { get; set; }
    }
}
