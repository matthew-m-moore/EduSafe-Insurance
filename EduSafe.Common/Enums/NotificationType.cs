namespace EduSafe.Common.Enums
{
    public enum NotificationType
    {
        Unknown = 0,

        EmailWelcome,
        EmailAccepted,
        EmailPleaseReapply,
        EmailPolicyDetails,
        EmailBilling,
        EmailDelinquency,
        EmailPolicyPendingCancellation,
        EmailPolicyCancelled,
        EmailClaimInstructions,
        EmailClaimAccepted,
        EmailClaimDenied,

        LetterPolicyDetails,
        LetterPolicyPendingCancellation,
        LetterPolicyCancelled,
        LetterClaimInstructions,
        LetterClaimAccepted,
        LetterClaimDenied,
    }
}
