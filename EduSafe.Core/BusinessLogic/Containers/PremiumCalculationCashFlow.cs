namespace EduSafe.Core.BusinessLogic.Containers
{
    public class PremiumCalculationCashFlow
    {
        public int Period { get; set; }

        public double DiscountFactor { get; set; }

        public double Premium { get; set; }
        public double ProbabilityAdjustedPremium { get; set; }

        public double ProbabilityAdjustedCostsAndFees { get; set; }
        public double ProbabilityAdjustedDropOutClaims { get; set; }
        public double ProbabilityAdjustedGradSchoolClaims { get; set; }
        public double ProbabilityAdjustedEarlyHireClaims { get; set; }
        public double ProbabilityAdjustedUnemploymentClaims { get; set; }

        public double TotalCostsAndClaims =>
            ProbabilityAdjustedCostsAndFees +
            ProbabilityAdjustedDropOutClaims +
            ProbabilityAdjustedGradSchoolClaims +
            ProbabilityAdjustedEarlyHireClaims +
            ProbabilityAdjustedUnemploymentClaims;

        public double TotalCashFlow => ProbabilityAdjustedPremium - TotalCostsAndClaims;
        public double DiscountedCashFlow => TotalCashFlow * DiscountFactor;

        public PremiumCalculationCashFlow() { }

        protected PremiumCalculationCashFlow(PremiumCalculationCashFlow premiumCalculationCashFlow)
        {
            Period = premiumCalculationCashFlow.Period;
            DiscountFactor = premiumCalculationCashFlow.DiscountFactor;

            Premium = premiumCalculationCashFlow.Premium;
            ProbabilityAdjustedPremium = premiumCalculationCashFlow.ProbabilityAdjustedPremium;

            ProbabilityAdjustedCostsAndFees = premiumCalculationCashFlow.ProbabilityAdjustedCostsAndFees;
            ProbabilityAdjustedDropOutClaims = premiumCalculationCashFlow.ProbabilityAdjustedDropOutClaims;
            ProbabilityAdjustedGradSchoolClaims = premiumCalculationCashFlow.ProbabilityAdjustedGradSchoolClaims;
            ProbabilityAdjustedEarlyHireClaims = premiumCalculationCashFlow.ProbabilityAdjustedEarlyHireClaims;
            ProbabilityAdjustedUnemploymentClaims = premiumCalculationCashFlow.ProbabilityAdjustedUnemploymentClaims;
        }
    }
}
