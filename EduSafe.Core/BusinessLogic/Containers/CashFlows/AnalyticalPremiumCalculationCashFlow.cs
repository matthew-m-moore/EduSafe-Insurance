namespace EduSafe.Core.BusinessLogic.Containers.CashFlows
{
    public class AnalyticalPremiumCalculationCashFlow : PremiumCalculationCashFlow
    {
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

        public AnalyticalPremiumCalculationCashFlow(PremiumCalculationCashFlow premiumCalculationCashFlow)
            : base(premiumCalculationCashFlow)
        { }

        private AnalyticalPremiumCalculationCashFlow(AnalyticalPremiumCalculationCashFlow analyticalPremiumCalculationCashFlow)
            : base(analyticalPremiumCalculationCashFlow)
        {
            PaidInPremiumDropOutAdjustment = analyticalPremiumCalculationCashFlow.PaidInPremiumDropOutAdjustment;
            PaidInPremiumGradSchoolAdjustment = analyticalPremiumCalculationCashFlow.PaidInPremiumGradSchoolAdjustment;
            PaidInPremiumEarlyHireAdjustment = analyticalPremiumCalculationCashFlow.PaidInPremiumEarlyHireAdjustment;
        }

        public override CashFlow Copy()
        {
            return new AnalyticalPremiumCalculationCashFlow(this);
        }

        public override void Scale(double scaleFactor)
        {
            base.Scale(scaleFactor);

            PaidInPremiumDropOutAdjustment *= scaleFactor;
            PaidInPremiumGradSchoolAdjustment *= scaleFactor;
            PaidInPremiumEarlyHireAdjustment *= scaleFactor;
        }

        public override void Aggregate(CashFlow cashFlow)
        {
            if (cashFlow is AnalyticalPremiumCalculationCashFlow analyticalPremiumCalculationCashFlow)
            {
                base.Aggregate(analyticalPremiumCalculationCashFlow);

                PaidInPremiumDropOutAdjustment += analyticalPremiumCalculationCashFlow.PaidInPremiumDropOutAdjustment;
                PaidInPremiumGradSchoolAdjustment += analyticalPremiumCalculationCashFlow.PaidInPremiumGradSchoolAdjustment;
                PaidInPremiumEarlyHireAdjustment += analyticalPremiumCalculationCashFlow.PaidInPremiumEarlyHireAdjustment;
            }
        }
    }
}
