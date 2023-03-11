namespace MilsatIMS.Enums
{
    public enum BlockerType
    {
        Technical,
        NonTechnical,
        None
    }

    public enum BlockerOrigin
    {
        Self,
        Mentor,
        TeamMember,
        Internet,
        Electricity,
        Others,
        None
    }

    public enum Timeline
    {
        Lt10,
        Gt10,
        None
    }

    public enum MentorRating
    {
        Bad,
        Fair,
        Good,
        None
    }

    public enum InternRating
    {
        One,
        Two,
        Three,
        Four,
        Five
    }

    public enum ReportStatus
    {
        Submitted,
        Pending,
        Undone,
        Overdue
    }
}
