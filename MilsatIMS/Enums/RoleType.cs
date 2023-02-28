using System.ComponentModel;

namespace MilsatIMS.Enums
{
    /// <summary>
    /// 0=Admin,1=Mentor,2=Intern
    /// </summary>
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
