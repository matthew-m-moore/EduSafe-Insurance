namespace EduSafe.Core.BusinessLogic.Containers
{
    public class AnalyticalPremiumCalculationCashFlow : PremiumCalculationCashFlow
    {
        public AnalyticalPremiumCalculationCashFlow(PremiumCalculationCashFlow premiumCalculationCashFlow)
            : base(premiumCalculationCashFlow)
        { }

        public double TotalCostsAndUnemploymentClaims =>
            ProbabilityAdjustedCostsAndFees +
            ProbabilityAdjustedUnemploymentClaims;

        // Note, since the default premium guess is unity, this value will already be normalized, as needed
        public double TotalPremiumBasedClaims =>
            ProbabilityAdjustedDropOutClaims +
            ProbabilityAdjustedGradSchoolClaims +
            ProbabilityAdjustedEarlyHireClaims;

        public double DiscountedTotalNumerator => TotalCostsAndUnemploymentClaims * DiscountFactor;
        public double DiscountedTotalDenominator => (ProbabilityAdjustedPremium - TotalPremiumBasedClaims) * DiscountFactor;
    }
}
