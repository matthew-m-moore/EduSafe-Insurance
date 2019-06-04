using System.Linq;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class PremiumComputationResultSummary
    {
        public int? ScenarioId { get; set; }
        public string ScenarioName { get; set; }

        public double CalculatedMonthlyPremium { get; set; }
        public double AverageCalculatedPremium { get; set; }

        public double AverageMonthlyPremiums { get; set; }
        public double AverageMonthlyCosts { get; set; }
        public double AverageMonthlyClaims { get; set; }
        public double AverageMonthlyEquity { get; set; }
        public double AverageTotalCashFlow { get; set; }

        public double AverageEnrolled { get; set; }

        public double TotalUnemployed { get; set; }
        public double TotalDroppedOut { get; set; }
        public double TotalGraduated { get; set; }
        public double TotalGradSchool { get; set; }
        public double TotalEarlyHire { get; set; }
        public double TotalEmployed { get; set; }

        public double WeightedAverageLife { get; set; }

        public double TotalLifetimePremiums { get; set; }
        public double TotalLifetimeCosts { get; set; }
        public double TotalLifetimeClaims { get; set; }
        public double LossViaClaimsRatio { get; set; }

        public PremiumComputationResultSummary(PremiumComputationResult premiumComputationResult)
        {
            ScenarioId = premiumComputationResult.ScenarioId;
            ScenarioName = premiumComputationResult.ScenarioName;

            CalculatedMonthlyPremium = premiumComputationResult.CalculatedMonthlyPremium;
            AverageCalculatedPremium = premiumComputationResult.PremiumCalculationCashFlows.Skip(1).Average(c => c.Premium);

            AverageMonthlyPremiums = premiumComputationResult.PremiumCalculationCashFlows.Skip(1).Average(c => c.ProbabilityAdjustedPremium);
            AverageMonthlyCosts = premiumComputationResult.PremiumCalculationCashFlows.Skip(1).Average(c => c.ProbabilityAdjustedCostsAndFees);
            AverageMonthlyClaims = premiumComputationResult.PremiumCalculationCashFlows.Skip(1).Average(c => c.TotalClaims);
            AverageMonthlyEquity = premiumComputationResult.PremiumCalculationCashFlows.Skip(1).Average(c => c.ProbabilityAdjustedEquity);
            AverageTotalCashFlow = premiumComputationResult.PremiumCalculationCashFlows.Skip(1).Average(c => c.TotalCashFlow);

            AverageEnrolled = premiumComputationResult.EnrollmentStateTimeSeries.Average(t => t.Enrolled);

            TotalUnemployed = premiumComputationResult.EnrollmentStateTimeSeries.Last().Unemployed;
            TotalDroppedOut = premiumComputationResult.EnrollmentStateTimeSeries.Last().DroppedOut;
            TotalGraduated = premiumComputationResult.EnrollmentStateTimeSeries.Last().Graduated;
            TotalGradSchool = premiumComputationResult.EnrollmentStateTimeSeries.Last().GradSchool;
            TotalEarlyHire = premiumComputationResult.EnrollmentStateTimeSeries.Last().EalyHire;
            TotalEmployed = premiumComputationResult.EnrollmentStateTimeSeries.Last().Employed;

            WeightedAverageLife = ComputeWeightedAverageLife(premiumComputationResult);

            TotalLifetimePremiums = premiumComputationResult.PremiumCalculationCashFlows.Sum(c => c.ProbabilityAdjustedPremium);
            TotalLifetimeCosts = premiumComputationResult.PremiumCalculationCashFlows.Sum(c => c.ProbabilityAdjustedCostsAndFees);
            TotalLifetimeClaims = premiumComputationResult.PremiumCalculationCashFlows.Sum(c => c.TotalClaims);

            LossViaClaimsRatio = TotalLifetimeClaims / TotalLifetimePremiums;
        }

        private double ComputeWeightedAverageLife(PremiumComputationResult premiumComputationResult)
        {
            var weightedAverageLifeNumerator = 0.0;
            foreach (var enrollmentStateTimeSeriesEntry in premiumComputationResult.EnrollmentStateTimeSeries)
                weightedAverageLifeNumerator += (enrollmentStateTimeSeriesEntry.Period * enrollmentStateTimeSeriesEntry.Enrolled);

            var weightedAverageLifeDenominator = premiumComputationResult.EnrollmentStateTimeSeries.Sum(t => t.Enrolled);

            return weightedAverageLifeNumerator / weightedAverageLifeDenominator;
        }
    }
}
