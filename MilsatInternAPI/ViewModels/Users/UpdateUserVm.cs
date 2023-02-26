using MilsatInternAPI.Enums;

namespace MilsatInternAPI.ViewModels.Users
{
    public class UpdateUserVm
    {
        public IFormFile ProfilePicture { get; set; }
        public string Bio { get; set; }
    }
}
