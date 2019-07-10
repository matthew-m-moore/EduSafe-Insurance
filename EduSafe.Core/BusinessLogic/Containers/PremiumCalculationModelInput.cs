using System.Collections.Generic;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class PremiumCalculationModelInput
    {
        public int? ScenarioId { get; set; }
        public string ScenarioName { get; set; }

        public double PremiumMargin { get; }
        public double PreviouslyPaidInPremiums { get; set; }

        public double UnemploymentCoverageAmount { get; }
        public double DropOutWarrantyCoverageAmount { get; }
        public int DropOutWarrantyCoverageMonths { get; }

        public InterestRateCurve DiscountRateCurve { get; private set; }

        public double? DropOutOptionCoveragePercentage { get; set; }
        public double? GradSchoolOptionCoveragePercentage { get; set; }
        public double? EarlyHireOptionCoveragePercentage { get; set; }

        public CompoundingConvention CompoundingConvention { get; private set; }

        public PremiumCalculationModelInput(
            double unemploymentCoverageAmount,
            double dropOutWarrantyCoverageAmount,
            int dropOutWarrantyCoverageMonths,
            InterestRateCurve discountRateCurve,
            double? dropOutOptionCoveragePercentage = null,
            double? gradSchoolOptionCoveragePercentage = null,
            double? earlyHireOptionCoveragePercentage = null,
            double premiumMargin = 0.0)
        {
            UnemploymentCoverageAmount = unemploymentCoverageAmount;
            DropOutWarrantyCoverageAmount = dropOutWarrantyCoverageAmount;
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
                UnemploymentCoverageAmount,
                DropOutWarrantyCoverageAmount,
                DropOutWarrantyCoverageMonths,
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
