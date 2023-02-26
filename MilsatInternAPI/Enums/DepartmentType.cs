using System.ComponentModel;

namespace MilsatInternAPI.Enums
{
    public enum TeamType
    {
        [Description("Backend")]
        Backend,
        [Description("Branding and Communication")]
        Branding,
        [Description("Community")]
        Community,
        [Description("Frontend")]
        Frontend,
        [Description("Mobile")]
        Mobile,
        [Description("Staff")]
        Staff,
        [Description("UIUX")]
        UIUX
    }
}
