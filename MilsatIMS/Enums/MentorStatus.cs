using System.ComponentModel;

namespace MilsatIMS.Enums
{
    /// <summary>
    /// 0=Free, 1=Occupied
    /// </summary>
    public enum MentorStatus
    {
        [Description("Can take in more interns")]
        Free,
        [Description("Has enough interns")]
        Occupied
    }
}
