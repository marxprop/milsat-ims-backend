using System.ComponentModel;

namespace MilsatInternAPI.Enums
{
    public enum MentorStatus
    {
        [Description("Can take in more interns")]
        Free,
        [Description("Has enough interns")]
        Occupied
    }
}
