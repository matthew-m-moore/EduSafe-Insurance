using System;
using EduSafe.Common;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class PremiumCalculationModelInput
    {
        public int? ScenarioId { get; set; }
        public string ScenarioName { get; set; }

        public double PremiumMargin { get; }
        public double AnnualIncomeCoverageAmount { get; }
        public int MonthsOfIncomeCoverage { get; }

        public double IncomeCoverageAmount => AnnualIncomeCoverageAmount * _fractionOfYearCovered;
        private double _fractionOfYearCovered => (double) MonthsOfIncomeCoverage / Constants.MonthsInOneYear;

        public double PreviouslyPaidInPremiums { get; set; }

        public InterestRateCurve DiscountRateCurve { get; private set; }

        public double? DropOutOptionCoveragePercentage { get; }
        public double? GradSchoolOptionCoveragePercentage { get; }
        public double? EarlyHireOptionCoveragePercentage { get; }

        public CompoundingConvention CompoundingConvention { get; private set; }

        public PremiumCalculationModelInput(
            double annualIncomeCoverageAmount,
            int monthsOfIncomeCoverage,
            InterestRateCurve discountRateCurve,
            double? dropOutOptionCoveragePercentage = null,
            double? gradSchoolOptionCoveragePercentage = null,
            double? earlyHireOptionCoveragePercentage = null,
            double premiumMargin = 0.0)
        {
            if (monthsOfIncomeCoverage <= 0)
                throw new Exception("ERROR: Months of income coverage must be greater than zero.");

            AnnualIncomeCoverageAmount = annualIncomeCoverageAmount;
            MonthsOfIncomeCoverage = monthsOfIncomeCoverage;
            DiscountRateCurve = discountRateCurve;
            PremiumMargin = premiumMargin;

            DropOutOptionCoveragePercentage = dropOutOptionCoveragePercentage;
            GradSchoolOptionCoveragePercentage = gradSchoolOptionCoveragePercentage;
            EarlyHireOptionCoveragePercentage = earlyHireOptionCoveragePercentage;

            SetCompoundingConvention();
        }

        public void SetCompoundingConvention(CompoundingConvention compoundingConvention = CompoundingConvention.Monthly)
        {
            CompoundingConvention = compoundingConvention;
        }

        public void SetDiscountRateCurve(InterestRateCurve discountRateCurve)
        {
            DiscountRateCurve = discountRateCurve;
        }
    }
}
