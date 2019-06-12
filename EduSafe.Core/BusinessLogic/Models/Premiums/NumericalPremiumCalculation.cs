﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using EduSafe.Common;
using EduSafe.Common.Enums;
using EduSafe.Common.Utilities;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Containers.CashFlows;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Models.Premiums
{
    public class NumericalPremiumCalculation : PremiumCalculation
    {
        private List<PremiumCalculationCashFlow> _calculatedCashFlows;
        private List<EnrollmentStateArray> _enrollmentStateTimeSeries;
        private const double _targetPrecision = 1e-12;

        public NumericalPremiumCalculation(PremiumCalculationModelInput premiumCalculationModelInput)
            : base (premiumCalculationModelInput)
        { }

        /// <summary>
        /// Returns a deep, member-wise copy of the object.
        /// </summary>
        public override PremiumCalculation Copy()
        {
            var copyOfPremiumCalculationModelInput = PremiumCalculationModelInput.Copy();
            return new NumericalPremiumCalculation(copyOfPremiumCalculationModelInput);
        }

        public override double CalculatePremium(List<EnrollmentStateArray> enrollmentStateTimeSeries, ServicingCostsModel servicingCostsModel)
        {
            ServicingCostsDataTable = servicingCostsModel.CalculateServicingCosts(enrollmentStateTimeSeries);
            SetEnrollmentStateTimeSeries(enrollmentStateTimeSeries);

            var targetValue = 0.0;
            var premium = NumericalSearchUtility.NewtonRaphsonWithBisection(
                    CalculateCashFlows,
                    targetValue,
                    _targetPrecision);

            SetCalculatedCashFlows();
            return premium;
        }

        public override void CalculateResultWithGivenPremium(List<EnrollmentStateArray> enrollmentStateTimeSeries, ServicingCostsModel servicingCostsModel, double premium)
        {
            ServicingCostsDataTable = servicingCostsModel.CalculateServicingCosts(enrollmentStateTimeSeries);
            SetEnrollmentStateTimeSeries(enrollmentStateTimeSeries);

            CalculateCashFlows(premium);
            SetCalculatedCashFlows();
        }

        protected void SetEnrollmentStateTimeSeries(List<EnrollmentStateArray> enrollmentStateTimeSeries)
        {
            _enrollmentStateTimeSeries = enrollmentStateTimeSeries;
        }

        protected void SetCalculatedCashFlows()
        {
            CalculatedCashFlows = _calculatedCashFlows;
        }

        protected double CalculateCashFlows(double premiumAmountGuess = 1.0)
        {
            var firstUnemploymentPeriod = _enrollmentStateTimeSeries.First(e => e[StudentEnrollmentState.GraduatedUnemployed] > 0.0).MonthlyPeriod;
            var totalUnemploymentFraction = _enrollmentStateTimeSeries.Last().GetTotalState(StudentEnrollmentState.GraduatedUnemployed);
            var initialPeriodCashFlow = CreateInitialCashFlow(totalUnemploymentFraction, firstUnemploymentPeriod);

            _calculatedCashFlows = new List<PremiumCalculationCashFlow>();
            _calculatedCashFlows.Add(initialPeriodCashFlow);          

            foreach (var enrollmentStateArray in _enrollmentStateTimeSeries)
            {
                var monthlyPeriod = enrollmentStateArray.MonthlyPeriod;
                if (monthlyPeriod == 0) continue;

                var currentPeriodCashFlow = CalculateCashFlow(
                    enrollmentStateArray, 
                    ref totalUnemploymentFraction, 
                    premiumAmountGuess,
                    firstUnemploymentPeriod,
                    monthlyPeriod);

                _calculatedCashFlows.Add(currentPeriodCashFlow);
            }

            return _calculatedCashFlows.Sum(c => c.DiscountedCashFlow);
        }

        protected virtual PremiumCalculationCashFlow CreateInitialCashFlow(double totalUnemploymentFraction, int firstUnemploymentPeriod)
        {
            var initialPeriodCashFlow = new PremiumCalculationCashFlow
            {
                Period = 0,
                DiscountFactor = 1.0,

                Premium = PremiumCalculationModelInput.PreviouslyPaidInPremiums,
                ProbabilityAdjustedPremium = PremiumCalculationModelInput.PreviouslyPaidInPremiums,
                ProbabilityAdjustedCoverage = PremiumCalculationModelInput.IncomeCoverageAmount,
            };

            if (firstUnemploymentPeriod < Constants.MonthsInOneYear)
            {
                initialPeriodCashFlow.TotalLossReserves = totalUnemploymentFraction * PremiumCalculationModelInput.IncomeCoverageAmount;
                initialPeriodCashFlow.IncrementalLossReserves = initialPeriodCashFlow.TotalLossReserves;
            }

            return initialPeriodCashFlow;
        }

        protected virtual PremiumCalculationCashFlow CalculateCashFlow(
            EnrollmentStateArray enrollmentStateArray, 
            ref double totalUnemploymentFraction,
            double premiumAmountGuess, 
            int firstUnemploymentPeriod,
            int monthlyPeriod)
        {
            var servicingCosts = ServicingCostsDataTable.Rows[monthlyPeriod - 1];
            var discountFactor = CalculateDiscountFactor(monthlyPeriod);
            var costsAndFees = servicingCosts.Field<double>(Constants.TotalIdentifier);

            var dropOutCoverage = PremiumCalculationModelInput.DropOutOptionCoveragePercentage.GetValueOrDefault(0.0);
            var gradSchoolCoverage = PremiumCalculationModelInput.GradSchoolOptionCoveragePercentage.GetValueOrDefault(0.0);
            var earlyHireCoverage = PremiumCalculationModelInput.EarlyHireOptionCoveragePercentage.GetValueOrDefault(0.0);

            var unemploymentCoverage = PremiumCalculationModelInput.IncomeCoverageAmount;

            var enrollmentFraction = enrollmentStateArray.GetTotalState(StudentEnrollmentState.Enrolled);
            var dropOutFraction = enrollmentStateArray[StudentEnrollmentState.DroppedOut];
            var gradSchoolFraction = enrollmentStateArray[StudentEnrollmentState.GraduateSchool];
            var earlyHireFraction = enrollmentStateArray[StudentEnrollmentState.EarlyHire];
            var unemploymentFraction = enrollmentStateArray[StudentEnrollmentState.GraduatedUnemployed];
            totalUnemploymentFraction -= unemploymentFraction;

            var previouslyPaidInPremiums = PremiumCalculationModelInput.PreviouslyPaidInPremiums;
            var probabilityAdjustedCoverage = unemploymentCoverage * enrollmentFraction;
            var probabilityAdjustedPremium = premiumAmountGuess * enrollmentFraction;
            var probabilityAdjustedEquity = probabilityAdjustedPremium * PremiumCalculationModelInput.PremiumMargin;
            var totalPremiumsPaidIn = (monthlyPeriod * premiumAmountGuess) + previouslyPaidInPremiums;         

            var currentPeriodCashFlow = new PremiumCalculationCashFlow
            {
                Period = monthlyPeriod,
                DiscountFactor = discountFactor,

                Premium = premiumAmountGuess,
                ProbabilityAdjustedCoverage = probabilityAdjustedCoverage,
                ProbabilityAdjustedPremium = probabilityAdjustedPremium,
                ProbabilityAdjustedEquity = probabilityAdjustedEquity,
                ProbabilityAdjustedCostsAndFees = servicingCosts.Field<double>(Constants.TotalIdentifier),

                ProbabilityAdjustedDropOutClaims = dropOutCoverage * dropOutFraction * totalPremiumsPaidIn,
                ProbabilityAdjustedGradSchoolClaims = gradSchoolCoverage * gradSchoolFraction * totalPremiumsPaidIn,
                ProbabilityAdjustedEarlyHireClaims = earlyHireCoverage * earlyHireFraction * totalPremiumsPaidIn,

                ProbabilityAdjustedUnemploymentClaims = unemploymentCoverage * unemploymentFraction,
            };

            if (monthlyPeriod > (firstUnemploymentPeriod - Constants.MonthsInOneYear))
            {
                currentPeriodCashFlow.TotalLossReserves = unemploymentCoverage * totalUnemploymentFraction;
            }

            if (monthlyPeriod > 0)
            {
                var lastPeriodTotalLossReserves = _calculatedCashFlows[monthlyPeriod - 1].TotalLossReserves;
                var incrementalLossReserves = currentPeriodCashFlow.TotalLossReserves - lastPeriodTotalLossReserves;
                currentPeriodCashFlow.IncrementalLossReserves = incrementalLossReserves;
            }

            return currentPeriodCashFlow;
        }
    }
}
