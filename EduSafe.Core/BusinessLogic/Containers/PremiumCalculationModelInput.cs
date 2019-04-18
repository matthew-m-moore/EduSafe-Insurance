using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Returns a deep, member-wise copy of the object.
        /// </summary>
        public PremiumCalculationModelInput Copy()
        {
            var copyOfDiscountRateCurve = DiscountRateCurve.Copy();

            var copyOfDropOutOptionCoveragePercentage = DropOutOptionCoveragePercentage.HasValue
                ? new double? (DropOutOptionCoveragePercentage.Value)
                : null;
            var copyOfGradSchoolOptionCoveragePercentage = GradSchoolOptionCoveragePercentage.HasValue
                ? new double?(GradSchoolOptionCoveragePercentage.Value)
                : null;
            var copyOfEarlyHireOptionCoveragePercentage = EarlyHireOptionCoveragePercentage.HasValue
                ? new double?(EarlyHireOptionCoveragePercentage.Value)
                : null;

            return new PremiumCalculationModelInput(
                AnnualIncomeCoverageAmount,
                MonthsOfIncomeCoverage,
                copyOfDiscountRateCurve,
                copyOfDropOutOptionCoveragePercentage,
                copyOfGradSchoolOptionCoveragePercentage,
                copyOfEarlyHireOptionCoveragePercentage,
                PremiumMargin);
        }

        public void SetCompoundingConvention(CompoundingConvention compoundingConvention = CompoundingConvention.Monthly)
        {
            CompoundingConvention = compoundingConvention;
        }

        public void SetDiscountRateCurve(List<double> discountRateCurve)
        {
            SetDiscountRateCurve(new DataCurve<double>(discountRateCurve));
        }

        public void SetDiscountRateCurve(DataCurve<double> discountRateCurve)
        {
            DiscountRateCurve.RateCurve = discountRateCurve;
        }
    }
}
