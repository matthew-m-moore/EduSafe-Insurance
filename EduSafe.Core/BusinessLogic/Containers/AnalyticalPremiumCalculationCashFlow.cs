namespace EduSafe.Core.BusinessLogic.Containers
{
    public class AnalyticalPremiumCalculationCashFlow : PremiumCalculationCashFlow
    {
        public AnalyticalPremiumCalculationCashFlow(PremiumCalculationCashFlow premiumCalculationCashFlow)
            : base(premiumCalculationCashFlow)
        { }

        public double PaidInPremiumDropOutAdjustment { get; set; }
        public double PaidInPremiumGradSchoolAdjustment { get; set; }
        public double PaidInPremiumEarlyHireAdjustment { get; set; }

        public double TotalCostsAndUnemploymentClaims =>
            ProbabilityAdjustedCostsAndFees +
            ProbabilityAdjustedUnemploymentClaims;

        public double TotalPaidInPremiumAdjustments =>
            PaidInPremiumDropOutAdjustment +
            PaidInPremiumGradSchoolAdjustment +
            PaidInPremiumEarlyHireAdjustment;

        // Note, since the default premium guess is unity, this value will already be normalized, as needed
        // Unless there is a paid-in premium, where it will get appropriately adjusted downstream
        public double TotalPremiumBasedClaims =>
            ProbabilityAdjustedDropOutClaims +
            ProbabilityAdjustedGradSchoolClaims +
            ProbabilityAdjustedEarlyHireClaims;

        public double DiscountedTotalNumerator => (TotalCostsAndUnemploymentClaims + TotalPaidInPremiumAdjustments) * DiscountFactor;
        public double DiscountedTotalDenominator => (ProbabilityAdjustedPremium - TotalPremiumBasedClaims - ProbabilityAdjustedEquity) * DiscountFactor;
    }
}
