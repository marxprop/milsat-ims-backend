using System.ComponentModel;

namespace MilsatInternAPI.Enums
{
    public enum RoleType
    {
        [Description("Admin Role")]
        Admin,
        [Description("Mentor Role")]
        Mentor,
        [Description("Intern Role")]
        Intern
    }
}
