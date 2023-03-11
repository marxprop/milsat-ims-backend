using System.ComponentModel;

namespace MilsatIMS.Enums
{
    /// <summary>
    /// 0=Backend, 1=Branding, 2=Community, 3=Frontend, 4=Mobile, 5=Staff, 6=UI/UX, 7=None
    /// </summary>
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
        UIUX,
        [Description("None")]
        None
    }
}
