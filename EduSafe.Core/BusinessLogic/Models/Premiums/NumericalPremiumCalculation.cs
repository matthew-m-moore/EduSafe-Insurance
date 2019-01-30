﻿using System;
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
            ServicingCostsDataTable = servicingCostsModel.CalculateServicingCosts(enrollmentStateTimeSeries);
            SetEnrollmentStateTimeSeries(enrollmentStateTimeSeries);

            var targetValue = 0.0;
            var premium = NumericalSearchUtility.NewtonRaphsonWithBisection(
                    CalculateCashFlows,
                    targetValue,
                    floorValue: 0.0);

            SetCalculatedCashFlows();
            return premium;
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
            _calculatedCashFlows = new List<PremiumCalculationCashFlow>();
            var initialPeriodCashFlow = CreateInitialCashFlow();
            _calculatedCashFlows.Add(initialPeriodCashFlow);

            foreach (var enrollmentStateArray in _enrollmentStateTimeSeries)
            {
                var monthlyPeriod = enrollmentStateArray.MonthlyPeriod;
                if (monthlyPeriod == 0) continue;

                var currentPeriodCashFlow = CalculateCashFlow(enrollmentStateArray, premiumAmountGuess, monthlyPeriod);
                _calculatedCashFlows.Add(currentPeriodCashFlow);
            }

            return _calculatedCashFlows.Sum(c => c.DiscountedCashFlow);
        }

        protected virtual PremiumCalculationCashFlow CreateInitialCashFlow()
        {
            var initialPeriodCashFlow = new PremiumCalculationCashFlow
            {
                Period = 0,
                DiscountFactor = 1.0,

                Premium = _PremiumCalculationModelInput.PreviouslyPaidInPremiums,
                ProbabilityAdjustedPremium = _PremiumCalculationModelInput.PreviouslyPaidInPremiums,
            };

            return initialPeriodCashFlow;
        }

        protected virtual PremiumCalculationCashFlow CalculateCashFlow
            (EnrollmentStateArray enrollmentStateArray, double premiumAmountGuess, int monthlyPeriod)
        {
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
            var totalPremiumsPaidIn = monthlyPeriod * premiumAmountGuess;

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

            return currentPeriodCashFlow;
        }
    }
}