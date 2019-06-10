namespace EduSafe.Core.BusinessLogic.Containers.CashFlows
{
    public class PremiumCalculationCashFlow : CashFlow
    {
        public double DiscountFactor { get; set; }

        public double Premium { get; set; }
        public double ProbabilityAdjustedCoverage { get; set; }
        public double ProbabilityAdjustedPremium { get; set; }
        public double ProbabilityAdjustedEquity { get; set; }

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

        public double TotalClaims =>
            ProbabilityAdjustedDropOutClaims +
            ProbabilityAdjustedGradSchoolClaims +
            ProbabilityAdjustedEarlyHireClaims +
            ProbabilityAdjustedUnemploymentClaims;

        public double IncrementalLossReserves { get; set; }
        public double TotalLossReserves { get; set; }

        public double PremiumAvailableForReinvestment => ProbabilityAdjustedPremium - TotalCostsAndClaims - IncrementalLossReserves;

        public double TotalCashFlow => ProbabilityAdjustedPremium - TotalCostsAndClaims - ProbabilityAdjustedEquity;
        public double DiscountedCashFlow => TotalCashFlow * DiscountFactor;

        public PremiumCalculationCashFlow() { }

        protected PremiumCalculationCashFlow(PremiumCalculationCashFlow premiumCalculationCashFlow)
            : base(premiumCalculationCashFlow)
        {
            DiscountFactor = premiumCalculationCashFlow.DiscountFactor;

            Premium = premiumCalculationCashFlow.Premium;
            ProbabilityAdjustedCoverage = premiumCalculationCashFlow.ProbabilityAdjustedCoverage;
            ProbabilityAdjustedPremium = premiumCalculationCashFlow.ProbabilityAdjustedPremium;
            ProbabilityAdjustedEquity = premiumCalculationCashFlow.ProbabilityAdjustedEquity;

            ProbabilityAdjustedCostsAndFees = premiumCalculationCashFlow.ProbabilityAdjustedCostsAndFees;
            ProbabilityAdjustedDropOutClaims = premiumCalculationCashFlow.ProbabilityAdjustedDropOutClaims;
            ProbabilityAdjustedGradSchoolClaims = premiumCalculationCashFlow.ProbabilityAdjustedGradSchoolClaims;
            ProbabilityAdjustedEarlyHireClaims = premiumCalculationCashFlow.ProbabilityAdjustedEarlyHireClaims;
            ProbabilityAdjustedUnemploymentClaims = premiumCalculationCashFlow.ProbabilityAdjustedUnemploymentClaims;

            IncrementalLossReserves = premiumCalculationCashFlow.IncrementalLossReserves;
            TotalLossReserves = premiumCalculationCashFlow.TotalLossReserves;
        }

        public override CashFlow Copy()
        {
            return new PremiumCalculationCashFlow(this);
        }

        public override void Scale(double scaleFactor)
        {
            StudentCount *= scaleFactor;

            Premium *= scaleFactor;
            ProbabilityAdjustedCoverage *= scaleFactor;
            ProbabilityAdjustedPremium *= scaleFactor;
            ProbabilityAdjustedEquity *= scaleFactor;

            ProbabilityAdjustedCostsAndFees *= scaleFactor;
            ProbabilityAdjustedDropOutClaims *= scaleFactor;
            ProbabilityAdjustedGradSchoolClaims *= scaleFactor;
            ProbabilityAdjustedEarlyHireClaims *= scaleFactor;
            ProbabilityAdjustedUnemploymentClaims *= scaleFactor;

            IncrementalLossReserves *= scaleFactor;
            TotalLossReserves *= scaleFactor;
        }

        public override void Aggregate(CashFlow cashFlow)
        {
            if (cashFlow is PremiumCalculationCashFlow premiumCalculationCashFlow)
            {
                StudentCount += premiumCalculationCashFlow.StudentCount;

                Premium += premiumCalculationCashFlow.Premium;
                ProbabilityAdjustedCoverage += premiumCalculationCashFlow.ProbabilityAdjustedCoverage;
                ProbabilityAdjustedPremium += premiumCalculationCashFlow.ProbabilityAdjustedPremium;
                ProbabilityAdjustedEquity += premiumCalculationCashFlow.ProbabilityAdjustedEquity;

                ProbabilityAdjustedCostsAndFees += premiumCalculationCashFlow.ProbabilityAdjustedCostsAndFees;
                ProbabilityAdjustedDropOutClaims += premiumCalculationCashFlow.ProbabilityAdjustedDropOutClaims;
                ProbabilityAdjustedGradSchoolClaims += premiumCalculationCashFlow.ProbabilityAdjustedGradSchoolClaims;
                ProbabilityAdjustedEarlyHireClaims += premiumCalculationCashFlow.ProbabilityAdjustedEarlyHireClaims;
                ProbabilityAdjustedUnemploymentClaims += premiumCalculationCashFlow.ProbabilityAdjustedUnemploymentClaims;

                IncrementalLossReserves += premiumCalculationCashFlow.IncrementalLossReserves;
                TotalLossReserves += premiumCalculationCashFlow.TotalLossReserves;
            }
        }
    }
}
