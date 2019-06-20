namespace EduSafe.Common.Enums
{
    public enum FileVerificationStatusType
    {
        Unknown = 0,

        Uploaded,
        Pending,
        Verifying,
        PartiallyVerified,
        FullyVerified,
        Rejected,
    }
}
