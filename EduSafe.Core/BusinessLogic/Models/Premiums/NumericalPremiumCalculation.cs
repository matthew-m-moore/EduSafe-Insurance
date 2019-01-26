using System.Collections.Generic;
using System.Data;
using System.Linq;
using EduSafe.Common;
using EduSafe.Common.Enums;
using EduSafe.Common.Utilities;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Models.Premiums
{
    public class NumericalPremiumCalculation : PremiumCalculation
    {
        private List<PremiumCalculationCashFlow> _calculatedCashFlows;
        private List<EnrollmentStateArray> _enrollmentStateTimeSeries;

        

        public NumericalPremiumCalculation(PremiumCalculationModelInput premiumCalculationModelInput)
            : base (premiumCalculationModelInput)
        { }

        public override double CalculatePremium(List<EnrollmentStateArray> enrollmentStateTimeSeries, ServicingCostsModel servicingCostsModel)
        {
            _enrollmentStateTimeSeries = enrollmentStateTimeSeries;
            ServicingCostsDataTable = servicingCostsModel.CalculateServicingCosts(enrollmentStateTimeSeries);

            var targetValue = 0.0;
            var premium = NumericalSearchUtility.NewtonRaphsonWithBisection(
                    CalculateCashFlows,
                    targetValue,
                    floorValue: 0.0);

            CalculatedCashFlows = _calculatedCashFlows;
            return premium;
        }

        private double CalculateCashFlows(double premiumAmountGuess)
        {
            _calculatedCashFlows = new List<PremiumCalculationCashFlow>();

            var totalPremiumsPaidIn = 0.0;
            foreach (var enrollmentStateArray in _enrollmentStateTimeSeries)
            {
                var monthlyPeriod = enrollmentStateArray.MonthlyPeriod;
                if (monthlyPeriod == 0) continue;

                var servicingCosts = ServicingCostsDataTable.Rows[monthlyPeriod - 1];
                var discountFactor = CalculateDiscountFactor(monthlyPeriod);
                var costsAndFees = servicingCosts.Field<double>(Constants.TotalIdentifier);

                var dropOutCoverage = _PremiumCalculationModelInput.DropOutOptionCoveragePercentage.GetValueOrDefault(0.0);
                var gradSchoolCoverage = _PremiumCalculationModelInput.GradSchoolOptionCoveragePercentage.GetValueOrDefault(0.0);
                var earlyHireCoverage = _PremiumCalculationModelInput.EarlyHireOptionCoveragePercentage.GetValueOrDefault(0.0);

                var unemploymentCoverage = _PremiumCalculationModelInput.IncomeCoverageAmount;

                var enrollmentFraction = enrollmentStateArray.GetTotalState(StudentEnrollmentState.Enrolled);
                var dropOutFraction = enrollmentStateArray[StudentEnrollmentState.DroppedOut];
                var gradSchoolFraction = enrollmentStateArray[StudentEnrollmentState.GraduateSchool];
                var earlyHireFraction = enrollmentStateArray[StudentEnrollmentState.EarlyHire];
                var unemploymentFraction = enrollmentStateArray[StudentEnrollmentState.GraduatedUnemployed];

                var probabilityAdjustedPremium = premiumAmountGuess * enrollmentFraction;
                totalPremiumsPaidIn += premiumAmountGuess;

                var currentPeriodCashFlow = new PremiumCalculationCashFlow
                {
                    Period = monthlyPeriod,
                    DiscountFactor = discountFactor,

                    Premium = premiumAmountGuess,
                    ProbabilityAdjustedPremium = probabilityAdjustedPremium,
                    ProbabilityAdjustedCostsAndFees = servicingCosts.Field<double>(Constants.TotalIdentifier),

                    ProbabilityAdjustedDropOutClaims = dropOutCoverage * dropOutFraction * totalPremiumsPaidIn,
                    ProbabilityAdjustedGradSchoolClaims = gradSchoolCoverage * gradSchoolFraction * totalPremiumsPaidIn,
                    ProbabilityAdjustedEarlyHireClaims = earlyHireCoverage * earlyHireFraction * totalPremiumsPaidIn,

                    ProbabilityAdjustedUnemploymentClaims = unemploymentCoverage * unemploymentFraction
                };

                _calculatedCashFlows.Add(currentPeriodCashFlow);
            }

            return _calculatedCashFlows.Sum(c => c.DiscountedCashFlow);
        }
    }
}
