using System;
using System.Collections.Generic;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Containers.CashFlows;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.BusinessLogic.Models.Premiums
{
    public class AnalyticalPremiumCalculation : NumericalPremiumCalculation
    {
        private double _analyticalFormulaNumerator;
        private double _analyticalFormulaDenominator;

        public AnalyticalPremiumCalculation(PremiumCalculationModelInput premiumCalculationModelInput)
            : base(premiumCalculationModelInput)
        { }

        /// <summary>
        /// Returns a deep, member-wise copy of the object.
        /// </summary>
        public override PremiumCalculation Copy()
        {
            var copyOfPremiumCalculationModelInput = PremiumCalculationModelInput.Copy();
            return new AnalyticalPremiumCalculation(copyOfPremiumCalculationModelInput);
        }

        public override double CalculatePremium(List<EnrollmentStateArray> enrollmentStateTimeSeries, ServicingCostsModel servicingCostsModel)
        {
            _analyticalFormulaNumerator = (-1) * PremiumCalculationModelInput.PreviouslyPaidInPremiums;
            _analyticalFormulaDenominator = 0.0;

            ServicingCostsDataTable = servicingCostsModel.CalculateServicingCosts(enrollmentStateTimeSeries);
            SetEnrollmentStateTimeSeries(enrollmentStateTimeSeries);
            CalculateCashFlows();

            var premium = (_analyticalFormulaDenominator > 0.0)
                ? _analyticalFormulaNumerator / _analyticalFormulaDenominator
                : double.NaN;

            SetCalculatedCashFlows();
            return premium;
        }

        public override void CalculateResultWithGivenPremium(List<EnrollmentStateArray> enrollmentStateTimeSeries, ServicingCostsModel servicingCostsModel, double premium)
        {
            throw new Exception("ERROR: Cannot calculate cash flows with a given premium using the analytical premium calculation method. " +
                "Use the numerical calculation method if a given premium was provided.");
        }

        protected override PremiumCalculationCashFlow CreateInitialCashFlow()
        {
            var initialPeriodCashFlow = base.CreateInitialCashFlow();
            var initialPeriodAnalyticalCashFlow = new AnalyticalPremiumCalculationCashFlow(initialPeriodCashFlow);

            initialPeriodAnalyticalCashFlow.Premium = 0.0;
            initialPeriodAnalyticalCashFlow.ProbabilityAdjustedPremium = 0.0;

            return initialPeriodAnalyticalCashFlow;
        }

        protected override PremiumCalculationCashFlow CalculateCashFlow
            (EnrollmentStateArray enrollmentStateArray, double premiumAmountGuess, int monthlyPeriod)
        {
            var currentPeriodCashFlow  = base.CalculateCashFlow(enrollmentStateArray, premiumAmountGuess, monthlyPeriod);
            var analyticalPremiumCashFlow = new AnalyticalPremiumCalculationCashFlow(currentPeriodCashFlow);

            var previouslyPaidInPremiums = PremiumCalculationModelInput.PreviouslyPaidInPremiums;
            if (previouslyPaidInPremiums > 0.0)
                AdjustCashFlowForPaidInPremiums(analyticalPremiumCashFlow, enrollmentStateArray, previouslyPaidInPremiums, monthlyPeriod);

            _analyticalFormulaNumerator += analyticalPremiumCashFlow.DiscountedTotalNumerator;
            _analyticalFormulaDenominator += analyticalPremiumCashFlow.DiscountedTotalDenominator;

            return analyticalPremiumCashFlow;
        }

        private void AdjustCashFlowForPaidInPremiums(
            AnalyticalPremiumCalculationCashFlow analyticalPremiumCashFlow,
            EnrollmentStateArray enrollmentStateArray,
            double previouslyPaidInPremiums,
            int monthlyPeriod)
        {
            var dropOutCoverage = PremiumCalculationModelInput.DropOutOptionCoveragePercentage.GetValueOrDefault(0.0);
            var gradSchoolCoverage = PremiumCalculationModelInput.GradSchoolOptionCoveragePercentage.GetValueOrDefault(0.0);
            var earlyHireCoverage = PremiumCalculationModelInput.EarlyHireOptionCoveragePercentage.GetValueOrDefault(0.0);

            var dropOutFraction = enrollmentStateArray[StudentEnrollmentState.DroppedOut];
            var gradSchoolFraction = enrollmentStateArray[StudentEnrollmentState.GraduateSchool];
            var earlyHireFraction = enrollmentStateArray[StudentEnrollmentState.EarlyHire];

            analyticalPremiumCashFlow.ProbabilityAdjustedDropOutClaims = dropOutCoverage * dropOutFraction * monthlyPeriod;
            analyticalPremiumCashFlow.ProbabilityAdjustedGradSchoolClaims = gradSchoolCoverage * gradSchoolFraction * monthlyPeriod;
            analyticalPremiumCashFlow.ProbabilityAdjustedEarlyHireClaims = earlyHireCoverage * earlyHireFraction * monthlyPeriod;

            analyticalPremiumCashFlow.PaidInPremiumDropOutAdjustment = dropOutCoverage * dropOutFraction * previouslyPaidInPremiums;
            analyticalPremiumCashFlow.PaidInPremiumGradSchoolAdjustment = gradSchoolCoverage * gradSchoolFraction * previouslyPaidInPremiums;
            analyticalPremiumCashFlow.PaidInPremiumEarlyHireAdjustment = earlyHireCoverage * earlyHireFraction * previouslyPaidInPremiums;
        }
    }
}
